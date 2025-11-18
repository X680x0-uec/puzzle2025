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
                    AudioManager_TY.Instance.PlaySFX(colorChangeSound);
                }
            }
        }
    }
}