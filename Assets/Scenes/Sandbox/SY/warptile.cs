using UnityEngine;
using System.Collections.Generic; // Listを使うために追加

// インスペクタに表示するためのワープペアクラス
[System.Serializable]
public class WarpPair
{
    public Vector3Int sourceCell;      // ワープ元のセル座標
    public Vector3Int destinationCell; // ワープ先のセル座標
}

/// <summary>
/// プレイヤーがワープタイルに触れた時に、指定した別のタイルにワープさせるスクリプト
/// 移動中でもワープでき、ワープ後も同じ方向に移動を継続します
/// </summary>
public class warptile : MonoBehaviour
{
    public List<WarpPair> warpPairs = new List<WarpPair>(); // ワープペアのリスト
    public float warpThreshold = 0.1f; // プレイヤーがタイルの中心にどれだけ近づいたらワープするかの閾値
    private bool hasWarped = false; // 同じプレイヤーが連続してワープするのを防ぐフラグ
    private Grid grid; // グリッドの参照

    private void Start()
    {
        // 親オブジェクトからGridコンポーネントを取得
        grid = GetComponentInParent<Grid>();
        if (grid == null)
        {
            Debug.LogError("Grid component not found on the parents!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 新しいトリガーに入ったときにワープフラグをリセット
        hasWarped = false;

        // プレイヤーがタイルに触れた瞬間に即ワープさせる
        if (other.CompareTag("Player") && grid != null && !hasWarped)
        {
            // プレイヤーが現在いるセル座標を取得
            Vector3Int currentCell = grid.WorldToCell(other.transform.position);
            Vector3 currentCellCenter = grid.GetCellCenterWorld(currentCell);

            // 登録されているワープペアのリストをチェックする
            foreach (WarpPair pair in warpPairs)
            {
                if (pair.sourceCell == currentCell)
                {
                    Vector3 offset = other.transform.position - currentCellCenter;
                    Vector3 destCenter = grid.GetCellCenterWorld(pair.destinationCell);
                    other.transform.position = destCenter + offset;
                    hasWarped = true;
                    return;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasWarped && grid != null)
        {
            PlayerController_TY player = other.GetComponent<PlayerController_TY>();
            if (player != null)
            {
                // プレイヤーが現在いるセル座標を取得
                Vector3Int currentCell = grid.WorldToCell(other.transform.position);
                Vector3 currentCellCenter = grid.GetCellCenterWorld(currentCell);

                // プレイヤーがセルの中心に十分近いか
                float distance = Vector2.Distance(other.transform.position, currentCellCenter);

                if (distance <= warpThreshold)
                {
                    // 登録されているワープペアのリストをチェックする
                    foreach (WarpPair pair in warpPairs)
                    {
                        // プレイヤーがいるセルが、リスト内のどれかの「ワープ元」と一致するか？
                        if (pair.sourceCell == currentCell)
                        {
                            // 一致したら、ペアの「ワープ先」に移動
                            Vector3 offset = other.transform.position - currentCellCenter;
                            Vector3 destCenter = grid.GetCellCenterWorld(pair.destinationCell);
                            other.transform.position = destCenter + offset;
                            
                            hasWarped = true; // ワープ完了
                            return; // ループを抜ける
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // トリガーから出たときにワープフラグをリセット
        if (other.CompareTag("Player"))
        {
            hasWarped = false;
        }
    }

    private void OnValidate()
    {
        // ワープペアが設定されていない場合に警告を表示
        if (warpPairs == null || warpPairs.Count == 0)
        {
            Debug.LogWarning("No warp pairs set for " + gameObject.name);
        }
    }
}
