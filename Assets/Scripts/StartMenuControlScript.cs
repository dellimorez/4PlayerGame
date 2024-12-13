using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuControlScript : MonoBehaviour
{
    public GameObject text;
    public GameObject controls;

    private Button b;
    private bool showing = false;

    // Start is called before the first frame update
    void Start()
    {
        b = GetComponent<Button>();
        b.onClick.AddListener(ShowControls);
        showing = true;
        text.SetActive(false);
        controls.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (showing && Input.GetKeyUp(KeyCode.Return))
        {
            HideControls();
        }
    }

    void ShowControls() {
        showing = true;
        text.SetActive(true);
        controls.SetActive(true);    
    }

    void HideControls()
    {
        showing = false;
        text.SetActive(false);
        controls.SetActive(false);
    }
}
