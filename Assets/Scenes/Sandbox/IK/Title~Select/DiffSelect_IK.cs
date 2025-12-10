using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    [Header("ボタン設定")]
    public Button[] navButtons;

    [Header("Grid Settings")]
    public int columns = 5; 

    [Header("Sprite Settings")]
    public Sprite normalSprite;   
    public Sprite selectedSprite; 

    // ⭐ 追加: 決定音の設定
    [Header("Audio Settings")]
    public AudioClip moveSound;
    [Range(0f, 1f)] public float moveSoundScale = 1.0f;
    public AudioClip submitSound;
    [Range(0f, 1f)] public float soundVolume = 1.0f;

    private int currentIndex = 0;
    private InputList _inputSystem;

    void Start()
    {
        if (GameManager_TY.Instance == null) return;
        _inputSystem = GameManager_TY.Instance.inputList;
        if (_inputSystem != null) _inputSystem.Enable();

        // 画像リセット
        foreach (var button in navButtons)
        {
            if (button != null)
            {
                button.interactable = false;
                button.transition = Selectable.Transition.None;
                var img = button.GetComponent<Image>();
                if (img != null && normalSprite != null)
                {
                    img.sprite = normalSprite;
                    img.color = Color.white;
                }
            }
        }

        // 位置復元
        if (GameManager_TY.Instance.lastSelectedStageIndex != -1)
        {
            if (GameManager_TY.Instance.lastSelectedStageIndex < navButtons.Length)
            {
                currentIndex = GameManager_TY.Instance.lastSelectedStageIndex;
            }
        }

        SelectButton(currentIndex);
    }

    void Update()
    {
        if (_inputSystem == null) return;

        int nextIndex = currentIndex;

        if (_inputSystem.UI.Left.triggered)
            nextIndex = (currentIndex - 1 + navButtons.Length) % navButtons.Length;
        else if (_inputSystem.UI.Right.triggered)
            nextIndex = (currentIndex + 1) % navButtons.Length;
        else if (_inputSystem.UI.Up.triggered)
            nextIndex = (currentIndex - columns >= 0) ? currentIndex - columns : 0;
        else if (_inputSystem.UI.Down.triggered)
            nextIndex = (currentIndex + columns < navButtons.Length) ? currentIndex + columns : navButtons.Length - 1;

        if (nextIndex != currentIndex)
        {
            // ⭐ 追加: 移動音を鳴らす
            if (AudioManager_TY.Instance != null && moveSound != null)
            {
                AudioManager_TY.Instance.PlaySFX(moveSound, moveSoundScale);
            }

            if (navButtons[nextIndex] != null)
            {
                currentIndex = nextIndex;
                SelectButton(currentIndex);
            }
        }

        if (_inputSystem.UI.Submit.triggered)
        {
            if (currentIndex >= 0 && currentIndex < navButtons.Length && navButtons[currentIndex] != null)
            {
                if (GameManager_TY.Instance != null)
                {
                    GameManager_TY.Instance.lastSelectedStageIndex = currentIndex;
                }

                // ⭐ 追加: 決定音を鳴らす
                if (AudioManager_TY.Instance != null && submitSound != null)
                {
                    AudioManager_TY.Instance.PlaySFX(submitSound, soundVolume);
                }

                navButtons[currentIndex].onClick.Invoke();
            }
        }
    }

    void SelectButton(int index)
    {
        foreach (var button in navButtons)
        {
            if (button != null)
            {
                var img = button.GetComponent<Image>();
                if (img != null && normalSprite != null) img.sprite = normalSprite;
            }
        }

        if (index >= 0 && index < navButtons.Length)
        {
            var targetBtn = navButtons[index];
            if (targetBtn != null)
            {
                var img = targetBtn.GetComponent<Image>();
                if (img != null && selectedSprite != null) img.sprite = selectedSprite;
            }
        }
    }
}