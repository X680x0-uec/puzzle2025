using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    /*
    // Unityエディタからボタンを割り当てるためのリスト
    public Button[] navButtons;
    // Enterキーで実行するイベント
    public KeyCode submitKey = KeyCode.Return;

    [Header("Color Settings")]
    public Color normalColor = Color.white;
    public Color highlightedColor = Color.yellow;

    private int currentIndex = 0;
    */

    public GameObject firstSelectedButton;

    void Start()
    {
        StartCoroutine(SelectFirstButtonNextFrame());
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
    void Start()
    {
        // すべてのボタンのクリックを無効にし、TransitionをNoneに設定
        foreach (var button in navButtons)
        {
            if (button != null)
            {
                button.interactable = false;
                button.transition = Selectable.Transition.None;
            }
        }
        // ゲーム開始時に最初のボタンを選択状態にする
        SelectButton(currentIndex);
    }

    void Update()
    {
        // 左右キーの入力を検出
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentIndex = (currentIndex - 1 + navButtons.Length) % navButtons.Length;
            SelectButton(currentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentIndex = (currentIndex + 1) % navButtons.Length;
            SelectButton(currentIndex);
        }

        // Enterキーの入力を検出し、現在選択されているボタンのonClickイベントを実行
        if (Input.GetKeyDown(submitKey))
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
    */
}