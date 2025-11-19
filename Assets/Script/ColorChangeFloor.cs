using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 踏んだプレイヤーの色が変わるタイルのスクリプト
/// </summary>
public class ColorChangeFloor : MonoBehaviour
{
    public PlayerColor_TY.PlayerType newColor = PlayerColor_TY.PlayerType.Red;
    public AudioClip colorChangeSound;
    [Range(0f, 1f)] public float colorChangeSoundScale = 1.0f;

public Sprite spriteA; // 1つ目の画像（例：光ってない画像）
    public Color colorA = Color.white; // 1つ目の色
    
    public Sprite spriteB; // 2つ目の画像（例：光ってる画像）
    public Color colorB = Color.white; // 2つ目の色

    public float switchInterval = 0.5f; // 切り替わる秒数

    private SpriteRenderer spriteRenderer;
    private Quaternion initialRotation;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        initialRotation = transform.localRotation;
        
        // 画像が設定されていればアニメーションを開始
        if (spriteA != null && spriteB != null)
        {
            StartCoroutine(AnimateFloor());
        }
    }

    /// <summary>
    /// 一定時間ごとに画像と色を交互に切り替えるコルーチン
    /// </summary>
    private IEnumerator AnimateFloor()
    {
        while (true)
        {
            // パターンA
            spriteRenderer.sprite = spriteA;
            spriteRenderer.color = colorA;
            transform.localRotation = initialRotation;
            yield return new WaitForSeconds(switchInterval);

            // パターンB
            spriteRenderer.sprite = spriteB;
            spriteRenderer.color = colorB;
            transform.localRotation = initialRotation * Quaternion.Euler(0, 0, 180f);
            yield return new WaitForSeconds(switchInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerColor_TY player = other.GetComponent<PlayerColor_TY>();
            if (player != null)
            {
                player.SetType(newColor);
                if (AudioManager_TY.Instance != null && colorChangeSound != null)
                {
                    AudioManager_TY.Instance.PlaySFX(colorChangeSound, colorChangeSoundScale);
                }
            }
        }
    }
}