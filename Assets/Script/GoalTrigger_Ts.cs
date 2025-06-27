using UnityEngine;

/// <summary>
/// ゴールトリガーのスクリプト
/// プレイヤーがゴールに到達したかどうかのフラグのみを切り替える
/// 両方がゴールしているかはPlayerController_Ts.csで確認する
/// </summary>
public class GoalTrigger_Ts : MonoBehaviour
{
    public PlayerColor_Ts.PlayerType goalType; // ここにゴールできるプレイヤーのタイプ（色）
    private PlayerController_Ts playerController; // プレイヤーコントローラーの参照
    private PlayerColor_Ts playerColorScript; // プレイヤーカラーのスクリプト参照

    /// <summary>
    /// プレイヤーがゴールに到達したかどうかを判定する
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay2D(Collider2D other)
    {
        playerController = other.GetComponent<PlayerController_Ts>();
        playerColorScript = other.GetComponent<PlayerColor_Ts>();

        if (playerController != null && playerColorScript != null)
        {
            // ゴールできるプレイヤーのタイプと一致するか確認
            if (goalType == PlayerColor_Ts.PlayerType.None ||
                playerColorScript.mergedPlayerType == goalType) // Noneは共通ゴール
            {
                if (!playerController.isMoving)
                {
                    playerController.isGoal = true; // ゴールに到達した状態にする
                }
            }
        }
    }
}