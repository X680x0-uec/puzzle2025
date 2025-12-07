using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    // Unityエディタからボタンを割り当てるためのリスト
    public Button[] navButtons;
    
    // 1行に何個ボタンが並んでいるか
    [Header("Grid Settings")]
    public int columns = 5; 

    // 画像差し替え設定 (ここを追加！)
    [Header("Sprite Settings")]
    public Sprite normalSprite;   // 普段の画像
    public Sprite selectedSprite; // 選択されている時の画像

    private int currentIndex = 0;
    private InputList _inputSystem;

    void Start()
    {
        if (GameManager_TY.Instance == null)
        {
            Debug.LogError("GameManager_TY.Instanceが見つかりません。");
            return;
        }
        _inputSystem = GameManager_TY.Instance.inputList;
        
        if (_inputSystem != null)
        {
            _inputSystem.Enable();
        }

        // 初期化：すべてのボタンを普段の画像にする
        foreach (var button in navButtons)
        {
            if (button != null)
            {
                button.interactable = false;
                button.transition = Selectable.Transition.None;
                
                // 画像をリセット
                var img = button.GetComponent<Image>();
                if (img != null && normalSprite != null)
                {
                    img.sprite = normalSprite; // 色ではなく画像を戻す
                    img.color = Color.white;   // 色は白（画像そのまま）にする
                }
            }
        }
        
        // 最初の有効なボタンを探す
        if (navButtons.Length > 0 && navButtons[currentIndex] == null)
        {
            for (int i = 0; i < navButtons.Length; i++)
            {
                if (navButtons[i] != null)
                {
                    currentIndex = i;
                    break;
                }
            }
        }

        // 前回の位置を復元
        if (GameManager_TY.Instance != null && GameManager_TY.Instance.lastSelectedStageIndex != -1)
        {
            if (GameManager_TY.Instance.lastSelectedStageIndex < navButtons.Length)
            {
                currentIndex = GameManager_TY.Instance.lastSelectedStageIndex;
            }
        }

        SelectButton(currentIndex);
    }

    void Update()
    {
        if (_inputSystem == null) return;

        int nextIndex = currentIndex;

        // --- 移動入力処理 ---
        if (_inputSystem.UI.Left.triggered)
        {
            nextIndex = (currentIndex - 1 + navButtons.Length) % navButtons.Length;
        }
        else if (_inputSystem.UI.Right.triggered)
        {
            nextIndex = (currentIndex + 1) % navButtons.Length;
        }
        else if (_inputSystem.UI.Up.triggered)
        {
            if (currentIndex - columns >= 0) nextIndex = currentIndex - columns;
            else nextIndex = 0;
        }
        else if (_inputSystem.UI.Down.triggered)
        {
            if (currentIndex + columns < navButtons.Length) nextIndex = currentIndex + columns;
            else nextIndex = navButtons.Length - 1;
        }

        // 移動実行
        if (nextIndex != currentIndex)
        {
            if (navButtons[nextIndex] != null)
            {
                currentIndex = nextIndex;
                SelectButton(currentIndex);
            }
            else if (nextIndex == 0 || nextIndex == navButtons.Length - 1)
            {
                 // 空欄スキップ処理（簡易版）
            }
        }

        // 決定キー
        if (_inputSystem.UI.Submit.triggered)
        {
            if (currentIndex >= 0 && currentIndex < navButtons.Length && navButtons[currentIndex] != null)
            {
                if (GameManager_TY.Instance != null)
                {
                    GameManager_TY.Instance.lastSelectedStageIndex = currentIndex;
                }
                navButtons[currentIndex].onClick.Invoke();
            }
        }
    }

    void SelectButton(int index)
    {
        // 全ボタンを「普段の画像」に戻す
        foreach (var button in navButtons)
        {
            if (button != null)
            {
                var img = button.GetComponent<Image>();
                // 画像をNormalに戻す
                if (img != null && normalSprite != null) img.sprite = normalSprite;
            }
        }

        // 選択されたボタンだけ「選択用の画像」に変える
        if (index >= 0 && index < navButtons.Length)
        {
            var targetBtn = navButtons[index];
            if (targetBtn != null)
            {
                var img = targetBtn.GetComponent<Image>();
                // 画像をSelectedに変える
                if (img != null && selectedSprite != null) img.sprite = selectedSprite;
            }
        }
    }
}