using UnityEngine;
using UnityEngine.UI; // UIを使うために必要

public class ToggleImage2 : MonoBehaviour
{
    // Unityエディタから設定できるように public で宣言
    // Imageコンポーネント自体を参照する
    public Image targetImage;

    // ボタンが押されたときに呼び出す関数
    public void ToggleImageVisibility()
    {
        // targetImageオブジェクトが存在するか確認
        if (targetImage != null)
        {
            // ImageコンポーネントがアタッチされているGameObjectの
            // アクティブ/非アクティブを切り替える
            // isActiveAndEnabledはコンポーネントが有効かつGameObjectがアクティブかを示す
            bool isCurrentlyActive = targetImage.gameObject.activeSelf;

            // 現在アクティブなら非アクティブに、非アクティブならアクティブにする
            targetImage.gameObject.SetActive(!isCurrentlyActive);

            // 別の記述方法（短縮形）
            // targetImage.gameObject.SetActive(!targetImage.gameObject.activeSelf);
        }
        else
        {
            Debug.LogError("ターゲット画像が設定されていません。Inspectorを確認してください。");
        }
    }
}

