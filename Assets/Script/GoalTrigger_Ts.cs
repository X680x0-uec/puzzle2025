using UnityEngine;

public class GoalTrigger_Ts : MonoBehaviour
{
    public PlayerColor_Ts.PlayerType goalType; // ここにゴールできるプレイヤーのタイプ（色）
    private PlayerController_Ts playerController;
    private PlayerColor_Ts playerColorScript;

    private void OnTriggerStay2D(Collider2D other)
    {
        playerController = other.GetComponent<PlayerController_Ts>();
        playerColorScript = other.GetComponent<PlayerColor_Ts>();

        if (playerController != null && playerColorScript != null)
        {
            // ゴールできるプレイヤーのタイプと一致するか確認
            if (goalType == PlayerColor_Ts.PlayerType.None ||
                playerColorScript.mergedPlayerType == goalType)
            {
                if (!playerController.isMoving)
                {
                    playerController.isGoal = true; // ゴールに到達した状態にする
                }
            }
        }
    }
}