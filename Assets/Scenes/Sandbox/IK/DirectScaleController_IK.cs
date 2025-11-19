using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections; // コルーチンに必要

public class DirectScaleController_IK : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    // ⭐ Inspectorで設定する拡大率とアニメーション時間
    public Vector3 normalScale = new Vector3(1f, 1f, 1f);
    public Vector3 highlightedScale = new Vector3(1.2f, 1.2f, 1.2f); // 1.2倍に拡大
    public float scaleSpeed = 0.15f; // 拡大・縮小にかける時間（秒）
    public AudioClip scaleSound;

    private RectTransform rectTransform;
    private Coroutine scaleCoroutine;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError(gameObject.name + ": RectTransformが見つかりません。");
            enabled = false;
        }
        // 初期サイズを通常サイズに設定
        rectTransform.localScale = normalScale;
    }

    // ⭐ キーボード/マウスで選択状態になったとき（拡大）
    public void OnSelect(BaseEventData eventData)
    {
        if (AudioManager_TY.Instance != null && scaleSound != null)
        {
            AudioManager_TY.Instance.PlaySFX(scaleSound);
        }
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        // 拡大コルーチンを開始
        scaleCoroutine = StartCoroutine(ScaleTo(highlightedScale, scaleSpeed));
        Debug.Log(gameObject.name + " 拡大開始 (コード制御)");
    }

    // ⭐ 選択が解除されたとき（縮小）
    public void OnDeselect(BaseEventData eventData)
    {
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        // 縮小コルーチンを開始
        scaleCoroutine = StartCoroutine(ScaleTo(normalScale, scaleSpeed));
        Debug.Log(gameObject.name + " 縮小開始 (コード制御)");
    }

    // ⭐ スムーズに拡大・縮小を行うコルーチン
    private IEnumerator ScaleTo(Vector3 targetScale, float duration)
    {
        Vector3 startScale = rectTransform.localScale;
        float time = 0;

        while (time < duration)
        {
            rectTransform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            time += Time.unscaledDeltaTime; // Time.timeScale=0 でも動作するように unscaledDeltaTime を使う
            yield return null;
        }

        // 終了時にターゲットのスケールに正確に設定
        rectTransform.localScale = targetScale;
    }
}
