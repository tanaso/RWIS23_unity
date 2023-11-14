using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理のために必要

public class ChangeScene : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Debug.Log("SceneLoader.LoadScene() called");
        SceneManager.LoadScene(sceneName); // 指定されたシーン名でシーンをロード
    }
}
