using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject ResumeButton;
    public GameObject RestartButton;
    public GameObject ExitButton;
    public GameObject PauseButton;
    public GameObject Background;
    public GameObject TutorialImage;

    public static bool isPaused = false;

    private void Start()
    {
        ResumeButton.GetComponent<Button>().onClick.AddListener(UnpauseGame);
        RestartButton.GetComponent<Button>().onClick.AddListener(RestartGame);
        ExitButton.GetComponent<Button>().onClick.AddListener(ExitGame);
        PauseButton.GetComponent<Button>().onClick.AddListener(PauseGame);

        showPauseMenu(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        showPauseMenu(true);
    }

    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        showPauseMenu(false);
    }

    public void RestartGame()
    {
        UnpauseGame();
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        UnpauseGame();
        SceneManager.LoadScene("StartScene");
        isPaused = false;
    }

    private void showPauseMenu(bool active)
    {
        ResumeButton.SetActive(active);
        RestartButton.SetActive(active);
        ExitButton.SetActive(active);
        PauseButton.SetActive(!active);
        Background.SetActive(active);
        TutorialImage.SetActive(active);
    }
}
