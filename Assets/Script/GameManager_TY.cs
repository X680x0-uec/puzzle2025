using UnityEngine;
using TMPro;

public class GameManager_TY : MonoBehaviour
{
    // ゲーム全体の管理を行うシングルトン
    public static GameManager_TY Instance { get; private set; }
    public InputList inputList; // 入力リスト

    // --- ここから追加 ---
    public int moveCount = 0;
    public TMPro.TextMeshProUGUI countText;
    // --- ここまで追加 ---

    private void Awake()
    {
        // シングルトンの確立
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        inputList = new InputList();
        inputList.Enable();
    }    

    //ここからが追加部分
    private void OnDestroy()
    {
        //inputListが存在する場合、無効化する
        if (inputList != null)
        {
            inputList.Disable();
        }
    }
    //ここまでが追加部分

// --- ここから追加 ---
    /// <summary>
    /// 動いた回数をUIに表示する
    /// </summary>
    public void UpdateCountText()
    {
        if (countText != null)
        {
            countText.text = "Moves: " + moveCount.ToString();
        }
    }
    // --- ここまで追加 ---
    
    // ゲームの終了
    public void EndGame()
    {
        // 終了処理
        Debug.Log("Stage Completed!");
    }
}