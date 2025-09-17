using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestManager : MonoBehaviour
{
    public Button contButton;
    public Button upgButton;
    public Button restButton;
    public GameObject restCanvas;
    public static Boolean isRested;

    void Start()
    {
        upgButton.onClick.AddListener(() => {
            nextScreen();
        });
    }
    void Update()
    {
        if (restCanvas.activeSelf)
        {
            isRested = true;
            Time.timeScale = 0.0f;
        }
    }

    private void nextScreen()
    {
        Debug.Log("Upg button clicked!");
        SceneManager.LoadScene("Upgrade");
    }

    private void contScreen()
    {
        Debug.Log("cont button clicked!");
        SceneManager.LoadScene("Game");
    }
}
