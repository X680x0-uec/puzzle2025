using UnityEngine;

public class GameManager_Tsu : MonoBehaviour
{
    // ゲーム全体の管理を行うシングルトン
    public static GameManager_Tsu Instance { get; private set; }

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
    }

    // ゲームの開始
    public void StartGame()
    {
        // 初期化処理
        InitializePlayers();
        InitializeWalls();
    }

    // プレイヤーの初期化
    private void InitializePlayers()
    {
        // すべての PlayerColor_Tsu を取得（ソート不要の場合は高速）
        var players = Object.FindObjectsByType<PlayerColor_Tsu>(FindObjectsSortMode.None);
        foreach (var player in players)
        {
            // プレイヤーの色とタイプを設定
            player.SetPlayerType(Wall_Tsu.PlayerType.None);
            player.SetColor(Color.white);
        }
    }

    // 壁の初期化
    private void InitializeWalls()
    {
        var walls = Object.FindObjectsByType<Wall_Tsu>(FindObjectsSortMode.None);
        foreach (var wall in walls)
        {
            // 壁のタイプをランダムに設定
            wall.interactablePlayer = (Wall_Tsu.PlayerType)Random.Range(0, 3);
        }
    }

    // ゲームの終了
    public void EndGame()
    {
        // 終了処理
        Debug.Log("Game Over");
    }
}