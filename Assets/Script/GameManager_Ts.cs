using UnityEngine;

public class GameManager_Ts : MonoBehaviour
{
    // ゲーム全体の管理を行うシングルトン
    public static GameManager_Ts Instance { get; private set; }
    public InputList inputList; // 入力リスト

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

    // ゲームの終了
    public void EndGame()
    {
        // 終了処理
        Debug.Log("Stage Completed!");
    }
}