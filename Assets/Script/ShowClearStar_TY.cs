using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor; 
#endif

[ExecuteInEditMode]
public class ShowClearStar_TY : MonoBehaviour
{
    [Header("★ 星セットのPrefab")]
    public GameObject starSetPrefab;

    [Header("★ 画像設定")]
    [Tooltip("獲得したときの星の画像（明るい星）")]
    public Sprite earnedSprite; 
    [Tooltip("獲得していないときの星の画像（黒塗りの星）")]
    public Sprite emptySprite;

    [Header("自動設定")]
    [Tooltip("自動取得されたシーン名")]
    public string targetSceneNameString = ""; 

    [SerializeField, HideInInspector]
    private int _starCount = 0;

    [Header("位置調整")]
    public Vector3 offset = new Vector3(0, 30, -1); 
    public float scale = 20.0f; 

    // 画像の色（基本は白でOKですが、必要なら変えてください）
    public Color baseColor = Color.white;

    private GameObject currentStarSet;
    public List<Image> starImages = new List<Image>();

    void Start()
    {
        if (Application.isPlaying)
        {
            if (string.IsNullOrEmpty(targetSceneNameString)) AutoGetSceneNameFromButton();

            if (!string.IsNullOrEmpty(targetSceneNameString))
            {
                _starCount = PlayerPrefs.GetInt(targetSceneNameString + "_Stars", 0);
            }
        }
        DeployStarSet();
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (string.IsNullOrEmpty(targetSceneNameString)) AutoGetSceneNameFromButton();
        EditorApplication.delayCall += _DelayedDeployStars;
    }

    private void AutoGetSceneNameFromButton()
    {
        Button btn = GetComponent<Button>();
        if (btn == null) return;
        SerializedObject so = new SerializedObject(btn);
        SerializedProperty onClick = so.FindProperty("m_OnClick");
        SerializedProperty calls = onClick.FindPropertyRelative("m_PersistentCalls.m_Calls");

        for (int i = 0; i < calls.arraySize; i++)
        {
            SerializedProperty args = calls.GetArrayElementAtIndex(i).FindPropertyRelative("m_Arguments");
            string strArg = args.FindPropertyRelative("m_StringArgument").stringValue;
            if (!string.IsNullOrEmpty(strArg))
            {
                targetSceneNameString = strArg;
                break;
            }
        }
    }

    private void _DelayedDeployStars()
    {
        if (!Application.isPlaying) { EditorApplication.delayCall -= _DelayedDeployStars; DeployStarSet(); }
    }
#else
    private void AutoGetSceneNameFromButton() { }
#endif

    void DeployStarSet()
    {
        if (starSetPrefab == null) return;

        if (currentStarSet != null)
        {
            if (Application.isPlaying) Destroy(currentStarSet);
            else DestroyImmediate(currentStarSet);
        }
        
        foreach (Transform child in transform)
        {
            if (child.name.Contains("ClearStarSet"))
            {
                if (Application.isPlaying) Destroy(child.gameObject);
                else DestroyImmediate(child.gameObject);
            }
        }

        currentStarSet = Instantiate(starSetPrefab, transform);
        currentStarSet.name = "ClearStarSet";
        
        currentStarSet.transform.localPosition = offset;
        currentStarSet.transform.localScale = new Vector3(scale, scale, 1f);
        currentStarSet.transform.localRotation = Quaternion.identity;

        starImages.Clear();
        foreach (Transform child in currentStarSet.transform)
        {
            Image img = child.GetComponent<Image>();
            if (img != null) starImages.Add(img);
        }

        UpdateStarVisuals();
    }

    void UpdateStarVisuals()
    {
        int count = Mathf.Max(0, _starCount);

        Debug.Log($"星更新: {targetSceneNameString} = {count}個"); // 確認用

        for (int i = 0; i < starImages.Count; i++)
        {
            // 色は基本「白（画像そのまま）」にリセット
            starImages[i].color = baseColor;

            if (i < count)
            {
                // ⭐ 獲得済み：明るい星の画像にする
                if (earnedSprite != null) starImages[i].sprite = earnedSprite;
                // 万が一非表示になっていたら表示する
                starImages[i].gameObject.SetActive(true);
            }
            else
            {
                // ⭐ 未獲得：黒塗りの星の画像にする
                if (emptySprite != null)
                {
                    starImages[i].sprite = emptySprite;
                    starImages[i].gameObject.SetActive(true);
                }
                else
                {
                    // 黒星画像が設定されていない場合は非表示にする（旧仕様）
                    starImages[i].gameObject.SetActive(false);
                }
            }
        }
    }
}