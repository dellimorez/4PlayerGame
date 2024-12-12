using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeSceneOnTimer : MonoBehaviour
{
    public float changeTime;
    public string sceneName;

    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0 || Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
