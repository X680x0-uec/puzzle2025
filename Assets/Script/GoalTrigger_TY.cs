using UnityEngine;

/// <summary>
/// ゴールトリガーのスクリプト
/// プレイヤーがゴールに到達したかどうかのフラグのみを切り替える
/// 両方がゴールしているかはPlayerController_Ts.csで確認する
/// </summary>
public class GoalTrigger_TY : MonoBehaviour
{
    public PlayerColor_TY.PlayerType goalType; // ここにゴールできるプレイヤーのタイプ（色）
    private PlayerController_TY playerController; // プレイヤーコントローラーの参照
    private PlayerColor_TY playerColorScript; // プレイヤーカラーのスクリプト参照
    public bool isGoalUsed = false; // ゴールが使用されているかどうかのフラグ

    /// <summary>
    /// プレイヤーがゴールに到達したかどうかを判定する
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay2D(Collider2D other)
    {
        playerController = other.GetComponent<PlayerController_TY>();
        playerColorScript = other.GetComponent<PlayerColor_TY>();

        if (playerController != null && playerColorScript != null && !isGoalUsed)
        {
            // ゴールできるプレイヤーのタイプと一致するか確認
            if (goalType == PlayerColor_TY.PlayerType.None ||
                playerColorScript.originalPlayerType == goalType) // Noneは共通ゴール
            {
                if (!playerController.isMoving)
                {
                    isGoalUsed = true;
                    playerController.isGoal = true; // ゴールに到達した状態にする
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerController = other.GetComponent<PlayerController_TY>();

        if (playerController != null)
        {
            isGoalUsed = false;
            playerController.isGoal = false; // ゴールから離れたらゴール状態を解除
        }
    }
}