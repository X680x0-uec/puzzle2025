using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    // Unityエディタからボタンを割り当てるためのリスト
    public Button[] navButtons;
    // Enterキーで実行するイベント
    public KeyCode submitKey = KeyCode.Return;

    [Header("Color Settings")]
    public Color normalColor = Color.white;
    public Color highlightedColor = Color.yellow;

    private int currentIndex = 0;
    private InputList _inputSystem;

   void Start()
    {
        // 1. Input Systemへの参照を取得
        // ⭐ GameManager_TY.Instance がこのシーンで確実に存在していることを前提とする
        if (GameManager_TY.Instance == null)
        {
            Debug.LogError("GameManager_TY.Instanceが見つかりません。");
            return;
        }
        _inputSystem = GameManager_TY.Instance.inputList;
        
        // ⭐ ⭐ 追加: シーンがロードされたら、入力システムを有効化
       if (_inputSystem != null)
        {
        _inputSystem.Enable();
        }

        // 2. ボタンの初期設定
        foreach (var button in navButtons)
        {
            if (button != null)
            {
                button.interactable = false;
                button.transition = Selectable.Transition.None;
            }
        }
        
        // 3. 最初のボタンを選択状態にする
        SelectButton(currentIndex);
    }


    void Update()
    {
        // 左右キーの入力を検出
        if (_inputSystem.UI.Left.triggered)
        {
            currentIndex = (currentIndex - 1 + navButtons.Length) % navButtons.Length;
            SelectButton(currentIndex);
        }
        else if (_inputSystem.UI.Right.triggered)
        {
            currentIndex = (currentIndex + 1) % navButtons.Length;
            SelectButton(currentIndex);
        }

        // Enterキーの入力を検出し、現在選択されているボタンのonClickイベントを実行
        if (_inputSystem.UI.Submit.triggered)
        {
            navButtons[currentIndex].onClick.Invoke();
        }
    }

    void SelectButton(int index)
    {
        // すべてのボタンを通常の色に戻す
        foreach (var button in navButtons)
        {
            button.GetComponent<Image>().color = normalColor;
        }

        // 現在選択されているボタンの色をハイライトカラーに変更
        navButtons[index].GetComponent<Image>().color = highlightedColor;
    }
}