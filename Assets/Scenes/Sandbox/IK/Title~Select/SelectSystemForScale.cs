using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SelectSystemForScale : MonoBehaviour
{
    
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
}
