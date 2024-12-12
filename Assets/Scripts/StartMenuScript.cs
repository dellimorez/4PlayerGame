using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{

    private Button b;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        b = GetComponent<Button>();
        b.onClick.AddListener(StartGame);
    }

    void StartGame()
    { 
        SceneManager.LoadScene(sceneName);
    }
}
