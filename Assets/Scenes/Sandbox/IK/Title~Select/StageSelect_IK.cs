using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect_IK : MonoBehaviour
{
    /// <summary>
    /// ステージをロードする
    /// </summary>
    /// <param name="sceneName">ロードするシーンの名前</param>
    public void LoadStage(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
