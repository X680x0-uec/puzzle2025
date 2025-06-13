using UnityEngine;

/// <summary>
/// プレイヤーの色とタイプを管理
/// </summary>
public class PlayerColor_Tsu : MonoBehaviour
{
    // プレイヤーのタイプ（色）を定義
    public Wall_Tsu.PlayerType playerType;
    public Color playerColor;
    
    public void SetPlayerType(Wall_Tsu.PlayerType type)
    {
        playerType = type;
    }

    public void SetColor(Color newColor)
    {
        playerColor = newColor;
        // もしRendererがあれば、見た目の色も変える
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = newColor;
        }
    }
}