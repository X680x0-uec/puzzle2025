using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ToggleImage3 : MonoBehaviour
{
    public Image targetImage;
    private InputList _inputSystem;

    IEnumerator Hint3()
    {
        while (true)
        {
            if (_inputSystem.UI.Hint3.triggered)
            {

                bool isCurrentlyActive = targetImage.gameObject.activeSelf;

                targetImage.gameObject.SetActive(!isCurrentlyActive);


                yield return new WaitForSeconds(0.2f); // デバウンスのための待機時間
            }

            yield return null;
        }

    }

    void Start()
    {
        _inputSystem = GameManager_TY.Instance.inputList;
        StartCoroutine(Hint3());
    }

    public void ToggleImageVisibility()
    {

        if (targetImage != null)
        {

            bool isCurrentlyActive = targetImage.gameObject.activeSelf;

            targetImage.gameObject.SetActive(!isCurrentlyActive);
        }
        else
        {
            Debug.LogError("ターゲット画像が設定されていません。Inspectorを確認してください。");
        }


    }


}


