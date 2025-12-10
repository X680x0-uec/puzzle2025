using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Coroutineを使うために必要
public class PauseMenu_IK : MonoBehaviour
{
    public GameObject pausePanel; // 作成したポーズメニューのパネルをここにアタッチ
    public PauseMenuNavigator_IK pauseNavigator; // ここにナビゲーションスクリプトをアタッチ


    public bool isPaused = false;
    private bool inputDisabled = false; // 入力を無効化するフラグ

    [SerializeField] private string selectSceneName = "NormalStageSelect_IK"; // 発表用ステージ選択シーンの名前
    public AudioClip submitSound;
    [Range(0f, 1f)] public float soundVolume = 1.0f;

    private InputList _inputSystem;

    void Start()
    {
        _inputSystem = GameManager_TY.Instance.inputList;
    }

    void Update()
    {
        if (!isPaused && Time.timeScale == 0f)
        {
            
            return;
        }
        
        if (_inputSystem.UI.Pause.triggered && isPaused)
        {
            ResumeGame();
        }
        // 入力が無効化されていない、かつEscapeキーが押されたら
        if (!inputDisabled && _inputSystem.UI.Pause.triggered)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // ゲームを一時停止する
    public void PauseGame()
    {
        // ポーズメニューを表示...
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        inputDisabled = true; // ポーズ中は入力を無効化
        
        // ナビゲーションスクリプトの初期化メソッドを呼び出す
        StartCoroutine(pauseNavigator.SelectFirstButtonNextFrame());
    }

    // ゲームを再開する
    public void ResumeGame()
    {
        if (AudioManager_TY.Instance != null && submitSound != null)
        {
            AudioManager_TY.Instance.PlaySFX(submitSound, soundVolume);
        }
        // ポーズメニューを非表示...
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        // 再開後1フレームだけ入力を無効化
        StartCoroutine(EnableInputAfterFrame());
    }

    // 次のフレームで入力を有効化する
    IEnumerator EnableInputAfterFrame()
    {
        // 1フレーム待つ
        yield return null; 
        inputDisabled = false;
    }

    // ゲームをリスタートする
    public void RestartGame()
    {
        if (AudioManager_TY.Instance != null && submitSound != null)
        {
            AudioManager_TY.Instance.PlaySFX(submitSound, soundVolume);
        }
        // ゲームの時間を元に戻す
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ステージ選択に戻る
    public void GoToSelect()
    {
        if (AudioManager_TY.Instance != null && submitSound != null)
        {
            AudioManager_TY.Instance.PlaySFX(submitSound, soundVolume);
        }
        // ゲームの時間を元に戻す
        Time.timeScale = 1f;
        // 「SelectScene」という名前のシーンに遷移
        SceneManager.LoadScene(selectSceneName);
    }

    // オプションメニューを開く（今回は例としてログを出力）
    public void OpenOptions()
    {
        if (AudioManager_TY.Instance != null && submitSound != null)
        {
            AudioManager_TY.Instance.PlaySFX(submitSound, soundVolume);
        }
        Debug.Log("Open Options Menu");
        // ここにオプションメニューを表示する処理を追加します
    }

}