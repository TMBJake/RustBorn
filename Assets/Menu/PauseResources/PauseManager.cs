using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public Button quitButton;
    public Button pauseButton;
    public Button resumeButton;
    public GameObject pauseCanvas;
    public GameObject uiCanvas;
    public GameObject cardView;
    public static Boolean isPaused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1.0f;
        pauseButton.onClick.AddListener(ShowPause);
        resumeButton.onClick.AddListener(() => {
            ShowGame();
        });
        quitButton.onClick.AddListener(QuitButton);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ShowGame()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        pauseCanvas.SetActive(false);
        cardView.SetActive(true);
        uiCanvas.SetActive(true);

        CardRegistry.Instance?.ResetAllHovers();

    }


    public void ShowPause()
    {
        isPaused = true;
        Time.timeScale = 0.0f;
        pauseCanvas.SetActive(true);
        cardView.SetActive(false);
        uiCanvas.SetActive(false);

    }
    public void QuitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
