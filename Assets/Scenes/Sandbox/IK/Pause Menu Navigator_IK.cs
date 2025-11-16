using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseMenuNavigator_IK : MonoBehaviour
{
    // ポーズメニューのボタンを格納する配列
    public Button[] menuButtons;
    
    // 現在選択されているボタンのインデックス
    public int selectedIndex = 0;

    // ポーズメニューの機能を呼び出すためのコントローラー
    // インスペクターでPauseMenuControllerスクリプトがアタッチされたオブジェクトをアタッチ
    public PauseMenu_IK pauseMenuController;

    private InputList _inputSystem;
    public GameObject firstSelectedButton;

    void Start()
    {
        _inputSystem = GameManager_TY.Instance.inputList;
    }

    // StageClearManager_IKからパクリ
    public IEnumerator SelectFirstButtonNextFrame()
    {
        yield return null; 

        if (firstSelectedButton != null && EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
            Debug.Log("UI操作開始: 最初のボタンにフォーカスを設定しました。");
        }
    }

    /*
    public void SelectInitialButton()
    {
    // selectedIndexを0にリセット
    selectedIndex = 0;
    
    // 最初のボタン（インデックス0）を選択状態にする
    if (menuButtons.Length > 0)
    {
        menuButtons[selectedIndex].Select();
    }
    }

    void Update()
    {
        if (_inputSystem.UI.Up.triggered)
        {
            Navigate(-1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Navigate(1);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            // ここで選択されたボタンに対応する関数を呼び出す
            switch (selectedIndex)
            {
                case 0:
                    pauseMenuController.ResumeGame();
                    break;
                case 1:
                    pauseMenuController.RestartGame();
                    break;
                case 2:
                    pauseMenuController.GoToSelect();
                    break;
                case 3:
                    pauseMenuController.OpenOptions();
                    break;
            }
        }
    }

    private void Navigate(int direction)
    {
        // インデックスを更新
        selectedIndex = (selectedIndex + direction + menuButtons.Length) % menuButtons.Length;
        
        // 新しいボタンを選択状態にする
        menuButtons[selectedIndex].Select();
    }
    */
}