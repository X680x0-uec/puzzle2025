using UnityEngine;

/// <summary>
/// プレイヤーの色とタイプを管理
/// </summary>
public class PlayerColor_TY : MonoBehaviour
{
    // プレイヤーのタイプを定義
    public enum PlayerType { None, Red, Blue, Purple, Yellow, Orange, Green, Button, BrokenWall, DirectionTile, WarpTile }
    public Sprite redSprite;
    public Sprite blueSprite;
    public Sprite purpleSprite;
    private SpriteRenderer spriteRenderer;
    public PlayerType firstPlayerType; // 初期のプレイヤータイプ（色）
    public PlayerType originalPlayerType; // マージ前のプレイヤータイプ（色）
    public PlayerType mergedPlayerType; // マージ後のプレイヤータイプ（色）
    public Color playerColor; // プレイヤーの色

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // 初期のプレイヤータイプに基づいて色を設定
        SetType(firstPlayerType);
    }

    public void SetType(PlayerType type)
    {
        switch (type)
        {
            case PlayerType.Red:
                if(redSprite != null) {
                    spriteRenderer.sprite = redSprite;
                    originalPlayerType = PlayerType.Red;
                    mergedPlayerType = PlayerType.Red;
                }
                break;
            case PlayerType.Blue:
                if(blueSprite != null) {
                    spriteRenderer.sprite = blueSprite;
                    originalPlayerType = PlayerType.Blue;
                    mergedPlayerType = PlayerType.Blue;
                }
                break;
            case PlayerType.Purple:
                if(purpleSprite != null) {
                    spriteRenderer.sprite = purpleSprite;
                    mergedPlayerType = PlayerType.Purple;
                }
                break;
            // 他のタイプに対するスプライト設定もここに追加可能
            default:
            Debug.LogWarning("No sprite assigned for this PlayerType: " + type);
                break;
        }
        spriteRenderer.color = Color.white;
    }

/*
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
            case PlayerType.Yellow:
                SetColorFromColor(Color.yellow);
                break;
            case PlayerType.Orange:
                SetColorFromColor(new Color(1f, 0.5f, 0f)); // オレンジ色
                break;
            case PlayerType.Green:
                SetColorFromColor(Color.green);
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
        else if (IsSimilarColor(newColor, Color.yellow))
        {
            originalPlayerType = PlayerType.Yellow;
            mergedPlayerType = PlayerType.Yellow;
        }
        else if (IsSimilarColor(newColor, new Color(1f, 0.5f, 0f)))
        {
            mergedPlayerType = PlayerType.Orange;
        }
        else if (IsSimilarColor(newColor, Color.green))
        {
            mergedPlayerType = PlayerType.Green;
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
    */
}