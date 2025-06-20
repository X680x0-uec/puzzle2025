using UnityEngine;

/// <summary>
/// プレイヤーの色とタイプを管理
/// </summary>
public class PlayerColor_Ts : MonoBehaviour
{
    // プレイヤーのタイプを定義
    public enum PlayerType { None, Red, Blue, Purple }
    // プレイヤーのタイプ（色）を定義
    // 初期のプレイヤータイプ（色）
    public PlayerType firstPlayerType; 
    // 元のプレイヤータイプ
    public PlayerType originalPlayerType;
    // マージされたプレイヤータイプ（色）
    public PlayerType mergedPlayerType;
    public Color playerColor;

    private void Start()
    {
        // 初期のプレイヤータイプに基づいて色を設定
        SetColorFromType(firstPlayerType);
    }

    public void SetColorFromType(PlayerType type)
    {
        switch (type)
        {
            case PlayerType.Red:
                SetColor(Color.red);
                break;
            case PlayerType.Blue:
                SetColor(Color.blue);
                break;
            case PlayerType.Purple:
                SetColor(Color.magenta); // 紫色
                break;
            default:
                SetColor(Color.white);
                break;
        }
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
            originalPlayerType = PlayerType.Red;
            mergedPlayerType = PlayerType.Red;
        }
        else if (IsSimilarColor(newColor, Color.blue))
        {
            originalPlayerType = PlayerType.Blue;
            mergedPlayerType = PlayerType.Blue;
        }
        else if (IsSimilarColor(newColor, Color.magenta))
        {
            mergedPlayerType = PlayerType.Purple;
        }
        else
        {
            mergedPlayerType = PlayerType.None;
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