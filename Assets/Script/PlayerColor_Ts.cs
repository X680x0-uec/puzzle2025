using UnityEngine;

/// <summary>
/// プレイヤーの色とタイプを管理
/// </summary>
public class PlayerColor_Ts : MonoBehaviour
{
    // プレイヤーのタイプ（色）を定義
    public Wall_Ts.PlayerType playerType;
    public Color playerColor;

    public void SetPlayerType(Wall_Ts.PlayerType type)
    {
        playerType = type;
    }

    public void SetColor(Color newColor)
    {
        playerColor = newColor;
        // もしRendererがあれば、見た目の色も変える
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = newColor;
        }
        
        // 色の近さでPlayerTypeを判定
        if (IsSimilarColor(newColor, Color.red))
        {
            playerType = Wall_Ts.PlayerType.Red;
        }
        else if (IsSimilarColor(newColor, Color.blue))
        {
            playerType = Wall_Ts.PlayerType.Blue;
        }
        else if (IsSimilarColor(newColor, Color.magenta))
        {
            playerType = Wall_Ts.PlayerType.Purple;
        }
        else
        {
            playerType = Wall_Ts.PlayerType.None;
        }
    }

    // 色の近さを判定するヘルパー
    private bool IsSimilarColor(Color a, Color b, float threshold = 0.1f)
    {
        return Mathf.Abs(a.r - b.r) < threshold &&
               Mathf.Abs(a.g - b.g) < threshold &&
               Mathf.Abs(a.b - b.b) < threshold;
    }
}