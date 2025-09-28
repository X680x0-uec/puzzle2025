using System.Collections;
using UnityEngine;

/// <summary>
/// 2体のキャラクターを同時に操作し、壁に当たるまで移動し続ける制御
/// </summary>
public class PlayerController_TY : MonoBehaviour
{
    public GameObject startPoint; // スタート地点を示すオブジェクトをアタッチ
    public bool isPlayerA = true; // プレイヤーAかどうか（同時操作用）
    public float moveSpeed = 5f; // 移動速度
    private Vector3 moveDirection; // 現在の移動方向
    public bool isMoving = false; // 移動中かどうかのフラグ
    public bool isGoal = false; // ゴールに到達したかどうか
    private float x, y; // 入力値
    public PlayerController_TY otherPlayer; // もう一方のプレイヤーをアタッチ

    private Rigidbody2D rb;
    private Renderer rend;
    private PlayerColor_TY colorScript;

    /// <summary>
    /// 初期化処理、シーンの開始時に呼ばれる
    /// </summary>
    /// <returns></returns>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //このスクリプトがついているオブジェクトに Rigidbody2D コンポーネントがあればそれを取得
        rend = GetComponent<Renderer>();
        colorScript = GetComponent<PlayerColor_TY>();

        if (isPlayerA == otherPlayer.isPlayerA)
        {
            Debug.LogError("isPlayerAは片方のプレイヤーだけTrueにしてください。");
            return;
        }

        // 初期位置に移動
        if (startPoint != null)
            transform.position = startPoint.transform.position;
        //ゴールに到達したかどうかチェックを開始
        if (isPlayerA)
            StartCoroutine(GoalCheckCoroutine());
        // 移動の入力受付を開始(プレイヤーAがプレイヤーBの分の入力も受け付ける)
        if (isPlayerA)
            StartCoroutine(Moving());
    }

    /// <summary>
    /// プレイヤーAのみが起動する関数で、両方のプレイヤーの移動を一括制御する
    /// </summary>
    /// <returns></returns>
    IEnumerator Moving()
    {
        while (true) // 入力受付ループ
        {
            // どちらも移動中でないなら入力を受け付ける
            if (!isMoving && otherPlayer != null && !otherPlayer.isMoving)
            {
                // 入力を取得、InputSystemを使用（操作の対応はMaterial->Inputに記載）
                Vector2 moveInput = GameManager_TY.Instance.inputList.Player.Move.ReadValue<Vector2>();
                x = moveInput.x;
                y = moveInput.y;
                // 入力値が0でない場合に移動方向を決定
                if (y > 0 && Mathf.Abs(y) > Mathf.Abs(x)) // 上方向
                {
                    MoveDirection(Vector2.up);
                }
                else if (y < 0 && Mathf.Abs(y) > Mathf.Abs(x)) // 下方向
                {
                    MoveDirection(Vector2.down);
                }
                else if (x < 0 && Mathf.Abs(x) > Mathf.Abs(y)) // 左方向
                {
                    MoveDirection(Vector2.left);
                }
                else if (x > 0 && Mathf.Abs(x) > Mathf.Abs(y)) // 右方向
                {
                    MoveDirection(Vector2.right);
                }
            }
            yield return null;
        }
    }

    /// <summary>
    /// 入力された方向に移動するメソッド
    /// プレイヤーAしか起動しない関数で、プレイヤーBも同じ方向に移動する
    /// </summary>
    /// <param name="direction"></param>
    public void MoveDirection(Vector2 direction)
    {
        // 入力された方向に移動
        if (direction != Vector2.zero)
        {
            TryMove(direction); // プレイヤーAの移動
            otherPlayer.TryMove(direction); // プレイヤーBの移動
        }
    }

    /// <summary>
    /// 指定された方向に移動を試みるメソッド
    /// </summary>
    /// <param name="direction"></param>
    void TryMove(Vector3 direction)
    {
        moveDirection = direction.normalized; // 入力された方向を正規化
        isMoving = true;
        isGoal = false; // ゴール状態をリセット
        StartCoroutine(MoveUntilWall());
    }
    /// <summary>
    /// 指定された方向に進み続け、壁に当たるまで移動するコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveUntilWall()
    {
        int wallLayer = LayerMask.GetMask("Wall"); // "Wall"レイヤーのみ検知

        while (true)
        {
            // プレイヤーの前方からRayを出す
            Vector2 rayOrigin = (Vector2)transform.position + (Vector2)moveDirection * 0.6f;
            // Raycastで前方に壁があるか判定
            Ray2D ray = new Ray2D(rayOrigin, moveDirection);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.8f, wallLayer);
            float distance = 0.5f; // 0.5マス分進む距離

            Debug.DrawRay(ray.origin, ray.direction * 0.8f, Color.red, 0.05f); // デバッグ用のRayを表示、Gameビューからシーンビューに切り替えると確認可能

            if (hit.collider != null)
            {
                //Debug.Log("Hit: " + hit.collider.name);
                Wall_TY wall = hit.collider.GetComponent<Wall_TY>(); //接触したオブジェクトがWall_TYコンポーネントを持っているなら取得
                if (wall != null)
                {
                    //Debug.Log("Hit Wall: " + wall.name);

                    // 共通壁なら止まる
                    if (wall.interactablePlayer == PlayerColor_TY.PlayerType.None)
                        break;
                    // 色付き壁で自分と同じ色ならすり抜ける
                    if (wall.interactablePlayer == colorScript.mergedPlayerType ||
                        wall.interactablePlayer == colorScript.originalPlayerType)
                    {
                        // すり抜けるので進み続ける
                    }
                    else
                    {
                        // すり抜け不可の色付き壁なら止まる
                        break;
                    }
                }
                else
                {
                    // 壁以外のオブジェクト,自分自身などは無視して進む
                }

                // 床の判定(IK)
                //Debug.Log("Hit: " + hit.collider.name);
                brokenfloor_IK floor = hit.collider.GetComponent<brokenfloor_IK>(); //接触したオブジェクトがbrokenfloor_IKコンポーネントを持っているなら取得
                if (floor != null)
                {

                    // 床の状態が崩壊なら止まる
                    if (floor.type == brokenfloor_IK.Type.notgo)
                        break;
                }

                // 向きタイルの判定(FH)
                DirectionChanger_FH dirChanger = hit.collider.GetComponent<DirectionChanger_FH>();
                if (dirChanger != null)
                {
                    // タイルの上まで行く
                    rb.MovePosition(transform.position + moveDirection * distance);
                    yield return new WaitForSeconds(0.05f);
                    rb.MovePosition(transform.position + moveDirection * distance);
                    yield return new WaitForSeconds(0.05f);
                    // 向きを変更
                    moveDirection = dirChanger.newDirection_FH.normalized;
                }
            }

            // 1マス進む
            rb.MovePosition(transform.position + moveDirection * distance);
            yield return new WaitForSeconds(0.05f);
            rb.MovePosition(transform.position + moveDirection * distance);
            yield return new WaitForSeconds(0.05f);
        }
        isMoving = false;
    }

    /// <summary>
    /// キャラクター同士が重なった時に色を混ぜる
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherChar = other.GetComponent<PlayerColor_TY>();
        if (otherChar != null && otherChar != this) // 他のキャラクターと重なった場合
        {
            if (isPlayerA) // プレイヤーAがプレイヤーBの分も色を同時に変更させる
            {
                // 色を混ぜる（単純な加算例）
                Color newColor = (colorScript.playerColor + otherChar.playerColor);
                colorScript.SetColorFromColor(newColor);
                otherChar.SetColorFromColor(newColor);
            }
        }
    }
    /// <summary>
    /// キャラクター同士が重なっていた状態から離れた時の処理
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        var otherChar = other.GetComponent<PlayerColor_TY>();
        if (otherChar != null && otherChar != this)
        {
            // 元の色に戻す処理
            colorScript.SetColorFromType(colorScript.originalPlayerType);
        }
    }

    /// <summary>
    /// ゴールチェックのコルーチン、プレイヤーAのみ起動
    /// </summary>
    /// <returns></returns>
    private System.Collections.IEnumerator GoalCheckCoroutine()
    {
        while (true)
        {
            // ゴール判定
            if (isGoal && otherPlayer.isGoal)
            {
                // ゲーム終了処理
                GameManager_TY.Instance.EndGame();
                yield break; // コルーチンを終了
            }
            yield return null;
        }
    }
}