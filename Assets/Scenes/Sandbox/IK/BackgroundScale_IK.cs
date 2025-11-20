using UnityEngine;

public class BackgroundScale_IK : MonoBehaviour
{
    void Start()
    {
        // メインカメラを取得
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            // カメラのInspectorにある「Size (Orthographic Size)」の値を取得
            float camSize = mainCamera.orthographicSize;

            // 要求された計算式: カメラのサイズ ÷ 5
            float targetScale = camSize / 5.0f;

            // 計算した値をこのオブジェクト（背景）のスケールに適用
            // 背景画像が歪まないよう、XとYに同じ値を入れます
            transform.localScale = new Vector3(targetScale, targetScale, 1f);
        }
        else
        {
            Debug.LogError("メインカメラが見つかりません。タグが 'MainCamera' になっているか確認してください。");
        }
    }
}