using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseMenuNavigator_IK : MonoBehaviour
{
    // ポーズメニューのボタンを格納する配列
    public Button[] menuButtons;
    
    // 現在選択されているボタンのインデックス
    private int selectedIndex = 0;

    // ポーズメニューの機能を呼び出すためのコントローラー
    // インスペクターでPauseMenuControllerスクリプトがアタッチされたオブジェクトをアタッチ
    public PauseMenu_IK pauseMenuController;

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
        if (Input.GetKeyDown(KeyCode.UpArrow))
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
}