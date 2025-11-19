using UnityEngine;
using UnityEngine.UI;

public class EnterKeyActivator : MonoBehaviour
{
    public Button myButton;
    private InputList _inputSystem;

    void Start()
    {
        _inputSystem = GameManager_TY.Instance.inputList;
    }

    void Update()
    {

        if (_inputSystem.UI.Esc.triggered)
        {
            myButton.onClick.Invoke();
        }
        else if (Input.anyKeyDown || _inputSystem.UI.Submit.triggered || _inputSystem.UI.Any.triggered)
        {
            myButton.onClick.Invoke();
        }
    }
}