using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{

    public GameObject loadCanvas;
    public GameObject menuCanvas;
    public TMP_Text loadFiles;
    public static bool isPaused = false;

    public Slider volumeSlider;
    public Slider brightnessSlider;
    public TMP_Text Volume;
    public TMP_Text Brightness;
    public Button loadButton;
    public Button escapeButton;

    void Start()
    {
        MenuManager.isMenu = false;
        loadButton.onClick.AddListener(ShowLoad);

        loadFiles.SetText("No Saved Files Found...\nWelcome to Rustborn!");
        //Escape button 
        escapeButton.onClick.AddListener(() => {
            ShowMenu();
        });
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenu();
        }
    }


    public void ShowMenu()
    {
        MenuManager.isMenu = true;
        loadCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void ShowLoad()
    {
        MenuManager.isMenu = false;
        menuCanvas.SetActive(false);
        loadCanvas.SetActive(true);
    }

}