using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController_IK : MonoBehaviour
{
    public Image blackScreen; // Canvas上の黒いImage
    public float fadeSpeed = 1.0f; // ★この値を変更して速度を調整します
    private void Start()
    {
        // シーン開始時にフェードイン
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    private IEnumerator FadeIn()
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.color = new Color(0, 0, 0, 1);
        
        while (blackScreen.color.a > 0)
        {
        blackScreen.color = new Color(0, 0, 0, blackScreen.color.a - Time.deltaTime * fadeSpeed);
        yield return null;
        }
        blackScreen.gameObject.SetActive(false);
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.color = new Color(0, 0, 0, 0);

        while (blackScreen.color.a < 1)
        {
        blackScreen.color = new Color(0, 0, 0, blackScreen.color.a + Time.deltaTime * fadeSpeed);
        yield return null;
        }

        // フェードアウト完了後にシーンをロード
        SceneManager.LoadScene(sceneName);
    }
}