using UnityEngine;

public class GameManager_TY : MonoBehaviour
{
    // ゲーム全体の管理を行うシングルトン
    public static GameManager_TY Instance { get; private set; }
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

    // ゲームの終了
    public void EndGame()
    {
        // 終了処理
        Debug.Log("Stage Completed!");
    }
}