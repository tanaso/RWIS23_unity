using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TaskListSceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void ChangeScene(string sceneName)
    {
        Debug.Log("sceneName" + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
