using UnityEngine;
using UnityEngine.UI; // レガシーな Text のために必要

public class StagenameSetting_IK : MonoBehaviour
{
    // 重要なポイント: Unityのインスペクターで設定したいステージ名を直接入力します
    [Header("ステージ設定")]
    [Tooltip("パズルゲームのステージとして表示したい名前を入力してください。")]
    public string stageDisplayName = "ステージ名未設定";

    // UIコンポーネントへの参照: ステージ名を表示するTextをアサインします
    [Header("UI設定")]
    public Text displayStageNameText; 

    void Start()
    {
        // 念のためUIコンポーネントが設定されているか確認
        if (displayStageNameText == null)
        {
            Debug.LogError("DisplayStageName Text が設定されていません。インスペクターでアサインしてください。");
            return;
        }

        // インスペクターで設定した「ステージ名」をUIに設定
        displayStageNameText.text = stageDisplayName;

        // デバッグログで確認
        Debug.Log("設定されたステージ名: " + displayStageNameText.text);
    }
}