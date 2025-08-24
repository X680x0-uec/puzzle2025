using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager_TY : MonoBehaviour
{
    public static GameManager_TY Instance { get; private set; }
    public InputList inputList;
    
    // UIの参照はコードで動的に取得
    public TMPro.TextMeshProUGUI countText;
    
    // 両方のプレイヤーの移動完了状態を追跡
    private bool playerAMoveFinished = false;
    private bool playerBMoveFinished = false;
    private int moveCount = 0;
    
    // プレイヤーの参照もコードで動的に取得
    private PlayerController_TY playerA;
    private PlayerController_TY playerB;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
        inputList = new InputList();
        inputList.Enable();
    }    

    private void OnDestroy()
    {
        if (inputList != null)
        {
            inputList.Disable();
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// シーンの読み込み完了時に呼ばれるメソッド
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
    // ステージ選択画面など、プレイヤーが存在しないシーンでは処理をスキップ
    if (scene.path.Contains("Assets/Scenes/Sandbox/IK/Title~Select/"))
    {
        Debug.Log("『Title~Select』フォルダ内のシーンです。プレイヤーの初期化をスキップします。");
        return; 
    }

        // 新しいシーンのUIとプレイヤーの参照を再取得
        countText = FindAnyObjectByType<TMPro.TextMeshProUGUI>();
        var players = FindObjectsByType<PlayerController_TY>(FindObjectsSortMode.None);
        
        if (players.Length >= 2)
        {
            foreach (var p in players)
            {
                if (p.isPlayerA)
                {
                    playerA = p;
                }
                else
                {
                    playerB = p;
                }
            }
            // プレイヤーの相互参照もここで設定
            if (playerA != null && playerB != null)
            {
                playerA.otherPlayer = playerB;
                playerB.otherPlayer = playerA;
            }
        }
        else
        {
            Debug.LogError("シーン内にプレイヤーオブジェクトが2つ見つかりません。");
        }

        // カウントをリセットしてUIを更新
        moveCount = 0;
        UpdateCountText();
        playerAMoveFinished = false;
        playerBMoveFinished = false;
    }
    
    public void UpdateCountText()
    {
        if (countText != null)
        {
            countText.text = "Moves: " + moveCount.ToString();
        }
    }

    /// <summary>
    /// プレイヤーが移動を終えたことを通知する
    /// </summary>
    public void ReportMoveFinished(bool isPlayerA)
    {
        if (isPlayerA)
        {
            playerAMoveFinished = true;
        }
        else
        {
            playerBMoveFinished = true;
        }

        if (playerAMoveFinished && playerBMoveFinished)
        {
            moveCount++;
            UpdateCountText();
            
            playerAMoveFinished = false;
            playerBMoveFinished = false;
        }
    }
    
    public void EndGame()
    {
        Debug.Log("Stage Completed!");
    }
}