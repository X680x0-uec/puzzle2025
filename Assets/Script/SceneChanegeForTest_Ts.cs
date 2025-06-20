using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移のテスト用スクリプト
/// </summary>
public class SceneChangeForTest_Ts : MonoBehaviour
{
    private void Update()
    {
        // シーン遷移のテスト
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScene("SampleScene");
        }
    }

    // シーン遷移を行うメソッド
    public void ChangeScene(string sceneName)
    {
        // シーン遷移処理
        Debug.Log("Changing scene to: " + sceneName);
        SceneManager.LoadScene(sceneName);
        // シーン遷移後の処理が必要な場合はここに追加
    }
}