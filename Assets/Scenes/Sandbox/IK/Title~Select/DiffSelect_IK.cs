using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    // Unityエディタからボタンを割り当てるためのリスト
    // ⭐ 重要: リストの順番は「左上」から始め、一番最後に「戻るボタン」を入れてください
    public Button[] navButtons;
    
    // 1行に何個ボタンが並んでいるか
    [Header("Grid Settings")]
    public int columns = 5; 

    // Enterキーで実行するイベント
    public KeyCode submitKey = KeyCode.Return;

    [Header("Color Settings")]
    public Color normalColor = Color.white;
    public Color highlightedColor = Color.yellow;

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
        
        // InputSystemの有効化
        if (_inputSystem != null)
        {
            _inputSystem.Enable();
        }

        // 初期化：すべてのボタンを選択解除状態にする
        foreach (var button in navButtons)
        {
            if (button != null)
            {
                button.interactable = false;
                button.transition = Selectable.Transition.None;
                var img = button.GetComponent<Image>();
                if (img != null) img.color = normalColor;
            }
        }
        
        // 最初の有効なボタンを探して選択する
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

        // TY
        if (GameManager_TY.Instance != null && GameManager_TY.Instance.lastSelectedStageIndex != -1)
        {
            // 配列の範囲内かチェックしてから適用
            if (GameManager_TY.Instance.lastSelectedStageIndex < navButtons.Length)
            {
                currentIndex = GameManager_TY.Instance.lastSelectedStageIndex;
            }
        }
        // TY ここまで

        SelectButton(currentIndex);
    }

    void Update()
    {
        if (_inputSystem == null) return;

        int nextIndex = currentIndex;

        // 左右キーの入力 (変更なし)
        if (_inputSystem.UI.Left.triggered)
        {
            nextIndex = (currentIndex - 1 + navButtons.Length) % navButtons.Length;
        }
        else if (_inputSystem.UI.Right.triggered)
        {
            nextIndex = (currentIndex + 1) % navButtons.Length;
        }
        
        // ⭐ 上キーの入力
        else if (_inputSystem.UI.Up.triggered)
        {
            // 通常の上移動ができる場合 (インデックスが列数分減らせる)
            if (currentIndex - columns >= 0)
            {
                nextIndex = currentIndex - columns;
            }
            else
            {
                // これ以上上に行けない場合は、強制的に「1問目（0番）」を選択
                nextIndex = 0;
            }
        }
        
        // ⭐ 下キーの入力
        else if (_inputSystem.UI.Down.triggered)
        {
            // 通常の下移動ができる場合 (インデックスが総数未満)
            if (currentIndex + columns < navButtons.Length)
            {
                nextIndex = currentIndex + columns;
            }
            else
            {
                // これ以上下に行けない場合は、強制的に「最後のボタン（戻るボタン）」を選択
                nextIndex = navButtons.Length - 1;
            }
        }

        // 移動処理
        if (nextIndex != currentIndex)
        {
            // 移動先が有効なボタンなら移動する
            if (navButtons[nextIndex] != null)
            {
                currentIndex = nextIndex;
                SelectButton(currentIndex);
            }
            // 移動先がnull（空欄）だった場合、0番か最後のボタンへの移動だけは許可する
            else if (nextIndex == 0 || nextIndex == navButtons.Length - 1)
            {
                 // ここで移動処理はスキップされるが、次の入力で正しい場所へ遷移することを期待する。
            }
        }

        // 決定キー
        if (_inputSystem.UI.Submit.triggered)
        {
            if (currentIndex >= 0 && currentIndex < navButtons.Length && navButtons[currentIndex] != null)
            {
                //TY
                if (GameManager_TY.Instance != null)
                {
                    GameManager_TY.Instance.lastSelectedStageIndex = currentIndex;
                }
                //TY ここまで
                navButtons[currentIndex].onClick.Invoke();
            }
        }
    }

    void SelectButton(int index)
    {
        // 色のリセット
        foreach (var button in navButtons)
        {
            if (button != null)
            {
                var img = button.GetComponent<Image>();
                if (img != null) img.color = normalColor;
            }
        }

        // 選択ボタンのハイライト
        if (index >= 0 && index < navButtons.Length)
        {
            var targetBtn = navButtons[index];
            if (targetBtn != null)
            {
                var img = targetBtn.GetComponent<Image>();
                if (img != null) img.color = highlightedColor;
            }
        }
    }
}