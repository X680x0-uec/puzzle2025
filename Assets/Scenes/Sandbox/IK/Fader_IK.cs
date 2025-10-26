using UnityEngine;
using UnityEngine.UI; // Imageコンポーネントにアクセスするために必要
using System.Collections; // コルーチンを使うために必要
using UnityEngine.SceneManagement;
public class Fader_IK : MonoBehaviour
{
    // フェードに使用するImageコンポーネント (黒い画面を覆うUI)
    private Image fadeImage;

    // フェードにかける時間
    public float fadeDuration = 0.5f;

    void Awake()
    {
        // アタッチされているImageコンポーネントを取得
        fadeImage = GetComponent<Image>();
        // フェード開始時に画面が暗くならないよう、透明度をゼロに設定
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
    }

    void Start()
    {
        // ★シーン開始時、画面が黒い状態から自動的にフェードインを開始
        StartFadeIn();
    }

    // ゲーム開始時などに呼ばれる、暗転からゲーム画面へ切り替える（フェードイン）処理
    public void StartFadeIn()
    {
        // 画面全体が黒い状態からスタート
        // Imageの色を完全に黒く、アルファ値を1f (不透明) に設定
        fadeImage.color = new Color(0f, 0f, 0f, 1f); 
        
        // 透明になっていくコルーチンを開始
        StartCoroutine(Fade(1f, 0f));
    }

    // シーン遷移前に呼ばれる、ゲーム画面から暗転へ切り替える（フェードアウト）処理
    public IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        // 完全に黒くなるまで待つ (透明度 0f から 1f へ)
        yield return StartCoroutine(Fade(0f, 1f));

        // フェードアウトが完了したら、シーンをロード
        SceneManager.LoadScene(sceneName);

        // シーンロード後、新しいシーンのStartFadeIn()が呼ばれてフェードインします
    }

    // フェード処理の本体（コルーチン）
    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timer = 0f;

        // Imageの色を現在の色から取得し、アルファ値を変更していく
        Color color = fadeImage.color;
        color.a = startAlpha;
        fadeImage.color = color;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime; // Time.timeScale=0でも動くようにunscaledDeltaTimeを使用
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            color.a = newAlpha;
            fadeImage.color = color;
            yield return null;
        }

        // 終了時に目標のアルファ値を確実に設定
        color.a = endAlpha;
        fadeImage.color = color;
    }
}