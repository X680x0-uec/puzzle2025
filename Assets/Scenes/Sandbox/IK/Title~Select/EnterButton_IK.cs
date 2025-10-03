using UnityEngine;
using UnityEngine.UI;

public class EnterKeyActivator : MonoBehaviour
{
    public Button myButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            myButton.onClick.Invoke();
        }
    }
}