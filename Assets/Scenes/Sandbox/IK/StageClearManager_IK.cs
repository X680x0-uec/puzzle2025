using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI; 

public class StageClearManager_IK : MonoBehaviour
{
    // ⭐ Inspectorから設定するフィールド
    public string stageSelectSceneName = "StageSelect"; 
    public GameObject clearScreenPanel;              
    public GameObject firstSelectedButton;           

    // ⭐ 星の評価に関連するフィールド
    public Image[] starImages;
    public Sprite goldenStarSprite; // 獲得した星のスプライト
    public Sprite dimStarSprite;    // 未獲得の星のスプライト (黒い星)
    
    // ⭐ 修正: ボタン群の親パネル (Inspectorで設定)
    public GameObject buttonGroupPanel; 
    
    [System.Serializable]
    public struct RatingThreshold
    {
        public int twoStarsMaxMoves; 
        public int threeStarsMaxMoves; 
    }
    public RatingThreshold currentStageThreshold;

    private int finalMoveCount = 0; 
    
    // GameManagerから最終移動回数を受け取り、クリア画面を表示する
    public void ShowClearScreen(int moves)
    {
        finalMoveCount = moves;
        
        // ⭐ 黒い星の初期化 
        if (starImages != null && dimStarSprite != null)
        {
            foreach (Image star in starImages)
            {
                if (star != null)
                {
                    // 星のGameObjectを強制的にアクティブにする
                    star.gameObject.SetActive(true); 
                    // すべて黒い星を設定
                    star.sprite = dimStarSprite;     
                }
            }
        }
        else
        {
            Debug.LogError("Dim Star Sprite または Star Images が Inspector で設定されていません！");
            return;
        }

        // ここでクリアパネルが表示される
        clearScreenPanel.SetActive(true);
        Time.timeScale = 0f; 

        // 評価ロジックを呼び出し、コルーチンを開始
        DisplayStarRating(finalMoveCount); 
    }
    

    // 評価を判定し、アニメーション開始コルーチンを呼び出す
    private void DisplayStarRating(int moves)
    {
        int starsEarned = 0;
        
        // 評価ロジック
        if (moves <= currentStageThreshold.threeStarsMaxMoves)
        {
            starsEarned = 3;
        }
        else if (moves <= currentStageThreshold.twoStarsMaxMoves)
        {
            starsEarned = 2;
        }
        else
        {
            starsEarned = 1;
        }

        Debug.Log("クリア回数: " + moves + " / 評価: " + starsEarned + "つ星を獲得しました。");
        
        // アニメーションコルーチンを直接起動する
        StartCoroutine(AnimateStarRating(starsEarned));
    }

    // ⭐ ⭐ 星のアニメーションと操作制御を統合したコルーチン ⭐ ⭐
    private IEnumerator AnimateStarRating(int starsToEarn)
    {
        // --- 1. アニメーション開始前に操作を無効化 (SetActive制御) ---
        if (buttonGroupPanel != null)
        {
            buttonGroupPanel.SetActive(false); 
        }

        // ⭐ 再初期化: ボタン無効化の副作用から星を保護するため、再度初期化
        if (starImages != null && dimStarSprite != null)
        {
            foreach (Image star in starImages)
            {
                if (star != null)
                {
                    star.sprite = dimStarSprite; 
                    star.gameObject.SetActive(true);
                }
            }
        }

        // --- 2. 黒い星の描画保証処理 (0.5秒の静止時間を確保) ---
        Canvas.ForceUpdateCanvases(); 
        yield return new WaitForEndOfFrame(); 
        
        // ⭐ 黒い星の静止時間
        yield return new WaitForSecondsRealtime(0.5f); 
        
        // --------------------------------------------------
        
        float delay = 0.3f; // 星が順番に光る間の間隔

        // 獲得した星の数だけ、順番に点灯させる
        for (int i = 0; i < starsToEarn; i++)
        {
            if (i < starImages.Length)
            {
                // 黒い星が黄色い星に切り替わる
                starImages[i].sprite = goldenStarSprite;

                // 最後の星が光った後は待機しない
                if (i < starsToEarn - 1)
                {
                    yield return new WaitForSecondsRealtime(delay);
                }
            }
        }
        
        // ⭐ 3. 操作を有効にする (SetActive制御)
        if (buttonGroupPanel != null)
        {
            buttonGroupPanel.SetActive(true);
        }
        
        // ⭐ 4. ボタン選択のコルーチンを起動
        StartCoroutine(SelectFirstButtonNextFrame());
    }
    
    // ボタンの初期フォーカス設定
    private IEnumerator SelectFirstButtonNextFrame()
    {
        yield return null; 

        if (firstSelectedButton != null && EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
            Debug.Log("UI操作開始: 最初のボタンにフォーカスを設定しました。");
        }
    }

    // 「リトライ」ボタンに割り当てるメソッド
    public void RestartStage()
    {
        Time.timeScale = 1f; 
        if (GameManager_TY.Instance != null && GameManager_TY.Instance.inputList != null)
        {
            GameManager_TY.Instance.inputList.Enable();
        }
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    // 「ステージ選択へ」ボタンに割り当てるメソッド
    public void LoadStageSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(stageSelectSceneName);
    }
    
    // ⭐ 削除された SetButtonsInteractable の代わりに関数残存を防ぐため、この位置には何も記述しない
}