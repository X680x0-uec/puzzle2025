using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor; 
#endif
// 【変更点】スクリプト名を StarCount_FH に変更
[ExecuteInEditMode]
public class StarCount_FH : MonoBehaviour
{
    // --- インスペクターで設定する項目 ---

    [Header("★ 星型Prefab設定")]
    [Tooltip("ここに星型のPrefabをドラッグ＆ドロップしてください。")]
    // 【変更点】weaponPrefab -> starPrefab
    public GameObject starPrefab;

    // 【変更点】変数を難易度と表示
    [Header("難易度 (星の数) 設定")]
    [Tooltip("この値が生成する星の数として使用されます。1以上の値を設定してください。")]
    // 【変更点】_difficultyLevel -> _starCount
    [SerializeField]
    private int _starCount = 4; // 内部変数：生成する星の数

    // 外部から星の数を取得するためのプロパティ (読み取り専用)
    // 【変更点】NumberOfWeapons -> StarCount
    public int StarCount
    {
        // 常に1以上の値を返すように保証
        get { return Mathf.Max(1, _starCount); }
    }

    [Header("横方向配置の設定")]
    public float spacingX = 1.0f;        // 星間の間隔（X軸方向）
    public float offsetY = -0.5f; // 自機から見て上下方向のオフセット（Y軸）
    public float spacingY = 1.0f;

    // 内部で使用する変数
    // 【変更点】weapons -> stars
    private List<GameObject> stars = new List<GameObject>();

    // --- Unityライフサイクルメソッド ---

    void Start()
    {
        DeployStars();
    }

    // StarCount_FH.cs の OnValidate メソッド (48行目付近)

    #if UNITY_EDITOR // エディタでのみ利用する機能は #if で囲む

    void OnValidate()
    {
        // 値をチェック（1未満にしない）
        _starCount = Mathf.Max(1, _starCount);
    
        // 【重要な修正】DeployStars()を直接呼ばず、次のエディタフレームで実行するようにスケジュールする
        // これにより、OnValidateの処理が安全に完了する
        EditorApplication.delayCall += _DelayedDeployStars;
    }

    /// <summary>
    /// OnValidateからの遅延実行用ラッパーメソッド
    /// </summary>
    private void _DelayedDeployStars()
    {
        // 遅延呼び出しが実行される際、Unityがプレイモードでないことを確認（二重実行防止）
        if (!Application.isPlaying)
        {
            // OnValidateが複数回呼ばれた場合、最後の呼び出しだけを実行するように、
            // 処理の実行後にキューから自分自身を削除しておく
            EditorApplication.delayCall -= _DelayedDeployStars;
        
            DeployStars(); // 遅延実行
        }
    }
    #endif // UNITY_EDITOR
    // --- 星の配置処理 ---

    /// <summary>
    /// 星をクリアし、指定された数だけ新しく生成して横一列に配置する
    /// </summary>
    void DeployStars()
    {
        // 【変更点】weaponPrefab -> starPrefab をチェック
        if (starPrefab == null)
        {
            Debug.LogWarning("Star Prefabが設定されていません。インスペクターに設定してください。");
            ClearStars();
            return;
        }

        // 既存の星を全て削除
        ClearStars();

        // 星の数
        int count = StarCount;
        if (count == 0) return;

        // 中央のオフセットを計算 (左右対称にするため)
        float totalWidth = (5 - 1) * spacingX;
        float startX = -totalWidth / 2.0f;

        for (int i = 0; i < count; i++)
        {
            // 星を自機の子オブジェクトとして生成
            // 【変更点】weapon -> star, weaponPrefab -> starPrefab
            GameObject star = Instantiate(starPrefab, transform);
            star.name = $"Star_{i:00}";
            stars.Add(star); // 【変更点】starsリストに追加

            // X軸の位置を計算
            float xOffset = startX + (i%5 * spacingX);

            // 星の位置を設定
            SetStarPosition(star, xOffset, offsetY - i / 5 * spacingY);

        }
    }

    /// <summary>
    /// 単一の星を自機からのオフセット位置に移動させる
    /// </summary>
    // 【変更点】SetWeaponPosition -> SetStarPosition
    void SetStarPosition(GameObject star, float xOffset, float yOffset)
    {
        // X軸は計算したオフセット、Y軸はoffsetYで高さを調整
        // 【変更点】verticalOffset -> offsetY
        star.transform.localPosition = new Vector3(xOffset, yOffset, 0f);
        star.transform.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// 既存の星オブジェクトを全て削除する
    /// </summary>
    // 【変更点】ClearWeapons -> ClearStars
    // StarCount_FH.cs

    /// <summary>
    /// 既存の星オブジェクトを全て削除する
    /// </summary>
    void ClearStars()
    {
        // starsリストをクリアしておく
        stars.Clear();

        // 子オブジェクトを格納するためのリスト
        List<GameObject> childrenToDestroy = new List<GameObject>();

        // このGameObject（親）の子を全てチェック
        foreach (Transform child in transform)
        {
            // ⭐︎ ここが重要な修正ポイント ⭐︎
            // DeployStars()で生成した星の名前は "Star_XX" と付けているため、
            // その名前で始まる子オブジェクトのみを削除対象とする
            if (child.name.StartsWith("Star_"))
            {
                childrenToDestroy.Add(child.gameObject);
            }

            // 【補足】星以外の特定の子オブジェクト（例: PlayerUI）を除外したい場合は、
            // else if (child.name == "PlayerUI") { continue; } 
            // のような除外条件を追加できます。
        }

        // リストアップした星オブジェクトを削除
        foreach (GameObject s in childrenToDestroy)
        {
            if (s != null)
            {
                if (Application.isPlaying)
                {
                    Destroy(s);
                }
                else
                {
                    // エディタモードでの即時削除
                    DestroyImmediate(s);
                }
            }
        }
    }
}