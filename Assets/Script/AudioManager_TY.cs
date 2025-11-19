using UnityEngine;
using UnityEngine.Audio;

public class AudioManager_TY : MonoBehaviour
{
    public static AudioManager_TY Instance { get; private set; }

    [Header("Audio Mixer")]
    public AudioMixer mainMixer; // 作成したMainMixerをアタッチ

    [Header("Audio Sources")]
    public AudioSource bgmSource; // BGM再生用
    public AudioSource sfxSource; // SFX再生用

    [Header("BGM")]
    public AudioClip mainBGM; // メインのBGMをアタッチ

    private void Awake()
    {
        // シングルトンとDontDestroyOnLoadの設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // BGMソースにミキサーの「BGM」グループを割り当て
        bgmSource.outputAudioMixerGroup = mainMixer.FindMatchingGroups("BGM")[0];
        // SFXソースにミキサーの「SFX」グループを割り当て
        sfxSource.outputAudioMixerGroup = mainMixer.FindMatchingGroups("SFX")[0];
    }

    private void Start()
    {
        // メインBGMの再生
        PlayBGM(mainBGM);
    }

    /// <summary>
    /// BGMを再生する
    /// </summary>
    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    /// <summary>
    /// 効果音（SFX）を一度だけ再生する
    /// </summary>
    public void PlaySFX(AudioClip clip, float volumeScale = 1.0f)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, volumeScale);
    }

    // --- 将来の音量調整のために、ミキサーの値を変更する関数 ---
    // (オプション画面でスライダーから呼び出す)
    // public void SetMasterVolume(float level)
    // {
    //     mainMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);
    // }
    // public void SetBGMVolume(float level)
    // {
    //     mainMixer.SetFloat("BGMVolume", Mathf.Log10(level) * 20f);
    // }
    // public void SetSFXVolume(float level)
    // {
    //     mainMixer.SetFloat("SFXVolume", Mathf.Log10(level) * 20f);
    // }
}