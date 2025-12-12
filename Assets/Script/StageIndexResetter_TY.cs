using UnityEngine;

public class StageIndexResetter_TY : MonoBehaviour
{
    /// <summary>
    /// ステージ選択のカーソル位置記憶をリセットする
    /// （難易度選択ボタンの OnClick にこれを追加してください）
    /// </summary>
    public void ResetIndex()
    {
        if (GameManager_TY.Instance != null)
        {
            // -1 をセットすると、ButtonManagerは「記憶なし」と判断して一番目を選択します
            GameManager_TY.Instance.lastSelectedStageIndex = -1;
            Debug.Log("ステージ選択カーソルをリセットしました");
        }
    }
}