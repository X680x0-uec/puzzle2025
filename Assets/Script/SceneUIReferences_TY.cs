using UnityEngine;
using TMPro; // TextMeshProを使う場合

/// <summary>
/// このシーンのUI参照をまとめるためのスクリプト。GameManagerは、まずこのコンポーネントを探し、ここから必要なUIコンポーネントを受け取る。
/// GameManagerに直接アタッチするのではなく、シーンごとに異なるUI参照をこのスクリプトで管理する。
/// </summary>
public class SceneUIReferences_TY : MonoBehaviour
{
    public UnityEngine.UI.Text moveCountTextLegacy;
}