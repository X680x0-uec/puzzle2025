using UnityEngine;

public class TitleExit_TY : MonoBehaviour
{
    public float requiredHoldTime = 0.5f; // 0.5秒長押し
    private float holdTimer = 0f;
    private InputList _inputSystem;

    void Start()
    {
        if (GameManager_TY.Instance != null)
        {
            _inputSystem = GameManager_TY.Instance.inputList;
        }
    }

    void Update()
    {
        // Escキーが押されている間
        if (_inputSystem != null && _inputSystem.UI.Esc.IsPressed())
        {
            holdTimer += Time.deltaTime;
            
            // デバッグログで確認（必要なら）
            // Debug.Log($"終了カウントダウン: {holdTimer}");

            if (holdTimer >= requiredHoldTime)
            {
                QuitGame();
            }
        }
        else
        {
            // 離したらリセット
            holdTimer = 0f;
        }
    }

    void QuitGame()
    {
        Debug.Log("ゲーム終了！");
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // エディタでの停止
        #else
            Application.Quit(); // 本番ビルドでの終了
        #endif
    }
}