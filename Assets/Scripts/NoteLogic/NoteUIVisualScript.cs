using UnityEngine;

public class NoteUIVisualScript : MonoBehaviour
{
    public GameObject noteUI;
    public GameObject noteText;
    public GameObject noteContent;

    private void Start()
    {
        noteText.SetActive(false);
        noteContent.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            CloseNote();
        }
    }

    public void View()
    {
        noteUI.SetActive(true);
        noteText.SetActive(true);
        noteContent.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseNote()
    {
        noteUI.SetActive(false);
        noteText.SetActive(false);
        noteContent.SetActive(false);
        Time.timeScale = 1f;
    }
}
