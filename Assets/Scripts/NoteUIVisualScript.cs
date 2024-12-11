using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NoteUIVisualScript : MonoBehaviour
{
    public GameObject noteUI;
    public GameObject noteText;

    private void Start()
    {
        noteText.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            noteUI.SetActive(false);
            noteText.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void view()
    {
        noteUI.SetActive(true);
        noteText.SetActive(true);
        Time.timeScale = 0f;
    }
}
