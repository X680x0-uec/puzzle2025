using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// 2体のキャラクターを同時に操作し、壁に当たるまで移動し続ける制御
/// </summary>
public class PlayerController_TY : MonoBehaviour
{
    public GameObject startPoint;
    public bool isPlayerA = true;
    public float moveSpeed = 5f;
    private Vector3 moveDirection;
    public bool isMoving = false;
    public bool isGoal = false;
    private float x, y;
    
    // otherPlayerはGameManagerで設定されるため、インスペクターでの手動設定は不要です。
    public PlayerController_TY otherPlayer; 

    // PauseMenuの参照は手動で設定
    public PauseMenu_IK pauseMenuController; 

    private Rigidbody2D rb;
    private Renderer rend;
    private PlayerColor_TY colorScript;

    private Vector3 startPosition; 
    public Vector3 otherPlayerStartPosition; 

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
        colorScript = GetComponent<PlayerColor_TY>();

        if (isPlayerA == otherPlayer.isPlayerA)
        {
            Debug.LogError("isPlayerAは片方のプレイヤーだけTrueにしてください。");
            return;
        }

        if (startPoint != null)
            transform.position = startPoint.transform.position;

        if (isPlayerA)
            StartCoroutine(GoalCheckCoroutine());
            
        if (isPlayerA)
            StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {
        while (true)
        {
            // 両方のプレイヤーの移動が停止するまで待機する
            yield return new WaitUntil(() => !isMoving && !otherPlayer.isMoving);

            if (pauseMenuController != null && pauseMenuController.isPaused)
            {
                yield return null;
                continue;
            }

            Vector2 moveInput = GameManager_TY.Instance.inputList.Player.Move.ReadValue<Vector2>();
            x = moveInput.x;
            y = moveInput.y;
            
            // 入力があった場合にのみ処理を実行
            if (x != 0 || y != 0)
            {
                // 移動開始前の座標を記録
                startPosition = transform.position;
                otherPlayer.startPosition = otherPlayer.transform.position;
                
                if (y > 0 && Mathf.Abs(y) > Mathf.Abs(x))
                {
                    MoveDirection(Vector2.up);
                }
                else if (y < 0 && Mathf.Abs(y) > Mathf.Abs(x))
                {
                    MoveDirection(Vector2.down);
                }
                else if (x < 0 && Mathf.Abs(x) > Mathf.Abs(y))
                {
                    MoveDirection(Vector2.left);
                }
                else if (x > 0 && Mathf.Abs(x) > Mathf.Abs(y))
                {
                    MoveDirection(Vector2.right);
                }

                // 両方のプレイヤーが移動を終えるまで待機する
                yield return new WaitUntil(() => !isMoving && !otherPlayer.isMoving);
                
                // プレイヤーAが、実際に移動が発生した場合にのみカウントを報告する
                if (isPlayerA)
                {
                    if (transform.position != startPosition || (otherPlayer != null && otherPlayer.transform.position != otherPlayer.startPosition))
                    {
                        if (GameManager_TY.Instance != null)
                        {
                            GameManager_TY.Instance.ReportMoveFinished(isPlayerA);
                        }
                    }
                }
            }

            yield return null;
        }
    }

<<<<<<< HEAD
    void MoveDirection(Vector2 direction)
=======
    /// <summary>
    /// 入力された方向に移動するメソッド
    /// プレイヤーAしか起動しない関数で、プレイヤーBも同じ方向に移動する
    /// </summary>
    /// <param name="direction"></param>
    public void MoveDirection(Vector2 direction)
>>>>>>> 699fb3f6dc34d2eb09394ecf6fa61394d2a9fee0
    {
        // otherPlayerがnullでないか確認
        if (direction != Vector2.zero && otherPlayer != null)
        {
            TryMove(direction);
            otherPlayer.TryMove(direction);
        }
    }

    void TryMove(Vector3 direction)
    {
        moveDirection = direction.normalized;
        isMoving = true;
        isGoal = false;
        StartCoroutine(MoveUntilWall());
    }

    IEnumerator MoveUntilWall()
    {
        Vector3 startPosition = transform.position;
        int wallLayer = LayerMask.GetMask("Wall");

        while (true)
        {
            Vector2 rayOrigin = (Vector2)transform.position + (Vector2)moveDirection * 0.6f;
            Ray2D ray = new Ray2D(rayOrigin, moveDirection);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.8f, wallLayer);
            float distance = 0.5f;

            Debug.DrawRay(ray.origin, ray.direction * 0.8f, Color.red, 0.05f);

            if (hit.collider != null)
            {
                Wall_TY wall = hit.collider.GetComponent<Wall_TY>();
                if (wall != null)
                {
                    if (wall.interactablePlayer == PlayerColor_TY.PlayerType.None)
                        break;
                    if (wall.interactablePlayer == colorScript.mergedPlayerType || wall.interactablePlayer == colorScript.originalPlayerType)
                    {
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    brokenfloor_IK floor = hit.collider.GetComponent<brokenfloor_IK>();
                    if (floor != null)
                    {
                        if (floor.type == brokenfloor_IK.Type.notgo)
                            break;
                    }
                }
            }

            rb.MovePosition(transform.position + moveDirection * distance);
            yield return new WaitForSeconds(0.05f);
            rb.MovePosition(transform.position + moveDirection * distance);
            yield return new WaitForSeconds(0.05f);
        }

        isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherChar = other.GetComponent<PlayerColor_TY>();
        if (otherChar != null && otherChar != this)
        {
            if (isPlayerA)
            {
                Color newColor = (colorScript.playerColor + otherChar.playerColor);
                colorScript.SetColorFromColor(newColor);
                otherChar.SetColorFromColor(newColor);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        var otherChar = other.GetComponent<PlayerColor_TY>();
        if (otherChar != null && otherChar != this)
        {
            colorScript.SetColorFromType(colorScript.originalPlayerType);
        }
    }
    
    private System.Collections.IEnumerator GoalCheckCoroutine()
    {
        while (true)
        {
            // otherPlayerがnullでないか確認
            if (otherPlayer != null && isGoal && otherPlayer.isGoal)
            {
                GameManager_TY.Instance.EndGame();
                yield break;
            }
            yield return null;
        }
    }
}