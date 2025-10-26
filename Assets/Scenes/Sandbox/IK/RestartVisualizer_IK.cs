using UnityEngine;
using UnityEngine.UI;

public class RestartVisualizer_IK : MonoBehaviour
{
    // ★SceneReloaderコンポーネントへの参照
    public SceneReloader_IK sceneReloader; 
    
    // 回転させる矢印のTransform (RectTransformはImageにアタッチされているはず)
    private RectTransform arrowRect;
    
    // 回転させる速さの最大値 (360度)
    private const float MAX_ROTATION_ANGLE = 360f;

    void Awake()
    {
        arrowRect = GetComponent<RectTransform>();

        // ★アタッチを忘れないように警告（オプション）
        if (sceneReloader == null)
        {
            Debug.LogError("SceneReloaderが設定されていません。Inspectorで設定してください。");
        }
    }

    void Update()
    {
        if (sceneReloader == null) return;

        // SceneReloaderから正確な長押し時間と必要時間を取得
        float holdDuration = sceneReloader.CurrentHoldTime;
        float requiredTime = sceneReloader.requiredHoldTime; // publicにしている前提

        // 進行度を計算 (0.0fから1.0fの間)
        float progress = Mathf.Clamp01(holdDuration / requiredTime);
        
        // 進行度に応じて0度から360度まで回転
        float rotationAngle = progress * MAX_ROTATION_ANGLE * 2 ;

        // 矢印の回転を適用（Z軸を中心に回転）
        // マイナスにすると右回転になります
        arrowRect.localRotation = Quaternion.Euler(0f, 0f, -rotationAngle); 

        // Rキーが押されていない（長押し時間が0）場合、自動的に0度にリセットされます
    }
}