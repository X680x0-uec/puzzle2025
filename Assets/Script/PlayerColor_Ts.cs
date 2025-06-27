using UnityEngine;

/// <summary>
/// プレイヤーの色とタイプを管理
/// </summary>
public class PlayerColor_Ts : MonoBehaviour
{
    // プレイヤーのタイプを定義
    public enum PlayerType { None, Red, Blue, Purple }
    public PlayerType firstPlayerType; // 初期のプレイヤータイプ（色）
    public PlayerType originalPlayerType; // マージ前のプレイヤータイプ（色）
    public PlayerType mergedPlayerType; // マージ後のプレイヤータイプ（色）
    public Color playerColor; // プレイヤーの色

    private void Start()
    {
        // 初期のプレイヤータイプに基づいて色を設定
        SetColorFromType(firstPlayerType);
    }

    /// <summary>
    /// プレイヤーのタイプに基づいて色を設定
    /// </summary>
    /// <param name="type"></param>
    public void SetColorFromType(PlayerType type)
    {
        switch (type)
        {
            case PlayerType.Red:
                SetColorFromColor(Color.red);
                break;
            case PlayerType.Blue:
                SetColorFromColor(Color.blue);
                break;
            case PlayerType.Purple:
                SetColorFromColor(Color.magenta); // 紫色
                break;
            default:
                SetColorFromColor(Color.white);
                break;
        }
    }

    /// <summary>
    /// プレイヤーの色を直接設定
    /// </summary>
    /// <param name="newColor"></param>
    public void SetColorFromColor(Color newColor)
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

    /// <summary>
    /// 色の近さを判定するヘルパー
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    private bool IsSimilarColor(Color a, Color b, float threshold = 0.1f)
    {
        return Mathf.Abs(a.r - b.r) < threshold &&
               Mathf.Abs(a.g - b.g) < threshold &&
               Mathf.Abs(a.b - b.b) < threshold;
    }
}