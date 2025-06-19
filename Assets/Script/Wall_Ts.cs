using UnityEngine;

/// <summary>
/// 壁の種類（共通壁、色付き壁）を管理
/// </summary>
public class Wall_Ts : MonoBehaviour
{
    public PlayerColor_Ts.PlayerType interactablePlayer = PlayerColor_Ts.PlayerType.None; // Noneは共通壁

    /*
    private void OnCollisionEnter(Collision collision)
    {
        var playerColor = collision.gameObject.GetComponent<PlayerColor_Ts>();
        if (playerColor != null)
        {
            // 色付き壁の場合、同じ色のプレイヤーはすり抜ける
            if (interactablePlayer != PlayerColor_Ts.PlayerType.None && playerColor.playerType == interactablePlayer)
            {
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            }
        }
    }
    */
}