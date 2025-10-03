using UnityEngine;

/// <summary>
/// 音を再生するためのクラス
/// </summary>
public class AudioPlayer_TY : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip playerMoveSound;

    void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayPlayerMoveSound()
    {
        if (audioSource != null && playerMoveSound != null)
        {
            audioSource.PlayOneShot(playerMoveSound);
        }
    }
}
