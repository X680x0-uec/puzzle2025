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


    void Update()
    {
        // 入力が無効化されていない、かつEscapeキーが押されたら
        if (!inputDisabled && Input.GetKeyDown(KeyCode.Escape))
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
        pauseNavigator.SelectInitialButton();
    }

    // ゲームを再開する
    public void ResumeGame()
    {
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
        // ゲームの時間を元に戻す
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ステージ選択に戻る
    public void GoToSelect()
    {
        // ゲームの時間を元に戻す
        Time.timeScale = 1f;
        // 「SelectScene」という名前のシーンに遷移
        SceneManager.LoadScene(selectSceneName);
    }

    // オプションメニューを開く（今回は例としてログを出力）
    public void OpenOptions()
    {
        Debug.Log("Open Options Menu");
        // ここにオプションメニューを表示する処理を追加します
    }

}