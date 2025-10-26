using UnityEngine;
using UnityEngine.SceneManagement; // シーンのロードに必要

public class SceneReloader_IK : MonoBehaviour
{
    public Fader_IK fader; 

    // Rキーが押されている時間を計測する変数
    private float holdTime = 0f;

    // リスタートが発動するまでの長押し時間（Inspectorで調整可能）
    [SerializeField]
    public float requiredHoldTime = 1.0f; 

    // Rキーが押されている時間を外部から取得できるようにするプロパティ
    public float CurrentHoldTime => holdTime;

    // リスタート時の画面演出に使うためのフラグ（オプション）
    private bool isHolding = false; 

    void Update()
    {
        // 1. Rキーが押され始めたか
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 長押しを開始
            isHolding = true;
            holdTime = 0f;
            Debug.Log("リスタート長押しを開始しました。");
        }

        // 2. Rキーが押されている間
        if (Input.GetKey(KeyCode.R) && isHolding)
        {
            // 時間を蓄積
            holdTime += Time.deltaTime; 

            // デバッグログで進行状況を確認
            // Debug.Log($"長押し時間: {holdTime:F2}秒"); 

            // 3. 必要な時間を超えたか
            if (holdTime >= requiredHoldTime)
            {
                // 現在のシーンを再ロード
                ReloadCurrentScene();
                // 発動したので長押しをリセット
                isHolding = false;
            }
        }

        // 4. Rキーが離されたか
        if (Input.GetKeyUp(KeyCode.R))
        {
            // 長押しを中断
            isHolding = false;
            holdTime = 0f; // ★ここで長押し時間をリセット
            Debug.Log("リスタート長押しを中断しました。");
        }
    }

    // シーンをロードする関数
    void ReloadCurrentScene()
{
    string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    
    if (fader != null)
    {
        // フェードアウトを開始し、完了後にシーンをロードするコルーチンを起動
        StartCoroutine(fader.FadeOutAndLoadScene(currentSceneName));
        Debug.Log($"リスタートのためフェードアウトを開始します: {currentSceneName}");
    }
    else
    {
        // Faderがない場合は即座にロード（保険）
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
    }
}
    
    // オプション: ポーズ中の誤作動を防ぐ（Time.timeScaleが0の時）
    // if (Time.timeScale > 0 && Input.GetKey(KeyCode.R) && isHolding) のように修正できます。
}