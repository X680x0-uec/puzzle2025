using UnityEngine;

/// <summary>
/// 2体のキャラクターを同時に操作し、壁に当たるまで移動し続ける制御
/// </summary>
public class PlayerController_Tsu : MonoBehaviour
{
    public Wall_Tsu.PlayerType playerType; // プレイヤーのタイプ（色）
    public float moveSpeed = 5f;             // 移動速度
    private Vector3 moveDirection;           // 現在の移動方向
    private bool isMoving = false;           // 移動中フラグ

    private Rigidbody rb;
    private Renderer rend;
    private PlayerColor_Tsu colorScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        colorScript = GetComponent<PlayerColor_Tsu>();
        // キャラクターの色を初期化
        if (colorScript != null)
            rend.material.color = colorScript.playerColor;
    }

    void Update()
    {
        // 移動中でなければ入力を受け付ける
        if (!isMoving)
        {
            // 矢印キーまたはWASDで移動方向を決定
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                TryMove(Vector3.forward);
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                TryMove(Vector3.back);
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                TryMove(Vector3.left);
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                TryMove(Vector3.right);
        }
    }

    // 指定方向に壁に当たるまで進み続ける
    void TryMove(Vector3 direction)
    {
        moveDirection = direction;
        isMoving = true;
        StartCoroutine(MoveUntilWall());
    }

    System.Collections.IEnumerator MoveUntilWall()
    {
        while (true)
        {
            // Raycastで前方に壁があるか判定
            Ray ray = new Ray(transform.position, moveDirection);
            RaycastHit hit;
            float distance = 0.6f; // 1マス分進む距離

            if (Physics.Raycast(ray, out hit, distance))
            {
                Wall_Tsu wall = hit.collider.GetComponent<Wall_Tsu>();
                if (wall != null)
                {
                    // 共通壁なら止まる
                    if (wall.interactablePlayer == Wall_Tsu.PlayerType.None)
                        break;
                    // 色付き壁で自分と同じ色ならすり抜ける
                    if (wall.interactablePlayer == playerType)
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
                    // 壁以外のオブジェクトなら止まる
                    break;
                }
            }

            // 1マス進む
            rb.MovePosition(transform.position + moveDirection);
            yield return new WaitForSeconds(0.05f);
        }
        isMoving = false;
    }

    // キャラクター同士が重なった時に色を混ぜる
    private void OnTriggerEnter(Collider other)
    {
        var otherChar = other.GetComponent<PlayerColor_Tsu>();
        if (otherChar != null && otherChar != this)
        {
            // 色を混ぜる（単純な加算例）
            colorScript.playerColor = (colorScript.playerColor + otherChar.playerColor) / 2f;
            rend.material.color = colorScript.playerColor;
        }
    }
}