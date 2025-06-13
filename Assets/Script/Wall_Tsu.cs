using UnityEngine;

/// <summary>
/// 壁の種類（共通壁、色付き壁）を管理
/// </summary>
public class Wall_Tsu : MonoBehaviour
{
    // プレイヤーのタイプを定義
    public enum PlayerType { None, PlayerA, PlayerB }
    public PlayerType interactablePlayer = PlayerType.None; // Noneは共通壁

    private void OnCollisionEnter(Collision collision)
    {
        var playerColor = collision.gameObject.GetComponent<PlayerColor_Tsu>();
        if (playerColor != null)
        {
            // 色付き壁の場合、同じ色のプレイヤーはすり抜ける
            if (interactablePlayer != PlayerType.None && playerColor.playerType == interactablePlayer)
            {
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            }
        }
    }
}