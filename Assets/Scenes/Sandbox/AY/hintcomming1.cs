using UnityEngine;
using UnityEngine.UI;

public class ToggleImage1 : MonoBehaviour
{
    public Image targetImage;


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


