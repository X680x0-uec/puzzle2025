using UnityEngine;
using UnityEngine.UI;

public class EnterKeyActivator : MonoBehaviour
{
    public Button myButton;
    private InputList _inputSystem;

    void Start()
    {
        _inputSystem = GameManager_TY.Instance.inputList;
    }

    void Update()
    {

        if (_inputSystem.UI.Esc.triggered)
        {
            QuitGame();
        }
        else if (_inputSystem.UI.Submit.triggered || _inputSystem.UI.Any.triggered)
        {
            myButton.onClick.Invoke();
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