using System.Collections;
using UnityEngine;

/// <summary>
/// 2体のキャラクターを同時に操作し、壁に当たるまで移動し続ける制御
/// </summary>
public class PlayerController_Ts : MonoBehaviour
{
    //public Wall_Ts.PlayerType playerType; // プレイヤーのタイプ（色）
    public bool isPlayerA = true; // プレイヤーAかどうか（同時操作用）
    public float moveSpeed = 5f;             // 移動速度
    private Vector3 moveDirection;           // 現在の移動方向
    public bool isMoving = false;           // 移動中フラグ
    public PlayerController_Ts otherPlayer; // もう一方のプレイヤー

    private Rigidbody2D rb;
    private Renderer rend;
    private PlayerColor_Ts colorScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
        colorScript = GetComponent<PlayerColor_Ts>();

        if (isPlayerA == otherPlayer.isPlayerA)
        {
            Debug.LogError("isPlayerAは片方のプレイヤーだけTrueにしてください。");
            return;
        }

        // 移動の入力受付を開始(プレイヤーAがプレイヤーBの入力も受け付ける)
        if (isPlayerA)
            StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {
        while (true)
        {
            // 移動中でなければ入力を受け付ける
            if (!isMoving && otherPlayer != null && !otherPlayer.isMoving)
            {
                // 矢印キーまたはWASDで移動方向を決定
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    TryMove(Vector2.up);
                    otherPlayer.TryMove(Vector2.up);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    TryMove(Vector2.down);
                    otherPlayer.TryMove(Vector2.down);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    TryMove(Vector2.left);
                    otherPlayer.TryMove(Vector2.left);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    TryMove(Vector2.right);
                    otherPlayer.TryMove(Vector2.right);
                }
            }
            yield return null;
        }
    }

    // 指定方向に壁に当たるまで進み続ける
    void TryMove(Vector3 direction)
    {
        moveDirection = direction.normalized; // 入力された方向を正規化
        isMoving = true;
        StartCoroutine(MoveUntilWall());
    }

    System.Collections.IEnumerator MoveUntilWall()
    {
        int wallLayer = LayerMask.GetMask("Wall"); // "Wall"レイヤーのみ検知

        while (true)
        {
            // Raycastで前方に壁があるか判定
            Ray2D ray = new Ray2D(transform.position, moveDirection);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.9f, wallLayer);
            float distance = 0.5f; // 1マス分進む距離

            Debug.DrawRay(ray.origin, ray.direction * 0.9f, Color.red, 0.05f);

            //if (Physics.Raycast(ray, out hit, distance))
            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.name);
                Wall_Ts wall = hit.collider.GetComponent<Wall_Ts>();
                if (wall != null)
                {
                    Debug.Log("Hit Wall: " + wall.name);

                    // 共通壁なら止まる
                    if (wall.interactablePlayer == PlayerColor_Ts.PlayerType.None)
                        break;
                    // 色付き壁で自分と同じ色ならすり抜ける
                    if (wall.interactablePlayer == colorScript.mergedPlayerType ||
                        wall.interactablePlayer == colorScript.originalPlayerType)
                    {
                        // すり抜けるので進み続ける
                    }
                    else
                    {
                        // すり抜け不可の壁なら止まる
                        break;
                    }
                }
                else
                {
                    // 壁以外のオブジェクトなら止まる?
                    //break;
                }
            }

            // 1マス進む
            rb.MovePosition(transform.position + moveDirection * distance);
            yield return new WaitForSeconds(0.05f);
        }
        isMoving = false;
    }

    // キャラクター同士が重なった時に色を混ぜる
    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherChar = other.GetComponent<PlayerColor_Ts>();
        if (otherChar != null && otherChar != this)
        {
            if (isPlayerA) // プレイヤーAが色を同時に変更させる
            {
                // 色を混ぜる（単純な加算例）
                Color newColor = (colorScript.playerColor + otherChar.playerColor);
                colorScript.SetColor(newColor);
                otherChar.SetColor(newColor);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // キャラクター同士が離れた時の処理
        // 例えば、色を元に戻すなど
        var otherChar = other.GetComponent<PlayerColor_Ts>();
        if (otherChar != null && otherChar != this)
        {
            // 元の色に戻す処理
            colorScript.SetColorFromType(colorScript.originalPlayerType);
        }
    }
}