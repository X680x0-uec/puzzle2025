using UnityEngine;

public class ForTestClearStar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete)) // Deleteキーなどで
       {
            PlayerPrefs.DeleteAll();
            Debug.Log("セーブデータを全消去しました！");
       }
    }
}
