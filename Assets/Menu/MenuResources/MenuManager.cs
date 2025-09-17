using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button startButton;
    public Button loadButton;
    public Button optionsButton;
    public Button quitButton;

    public static bool isMenu = false;

    void Start()
    {
        startButton.onClick.AddListener(OnStartClicked);
        loadButton.onClick.AddListener(OnLoadClicked);
        optionsButton.onClick.AddListener(OnOptionsClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
    }

    void OnStartClicked()
    {
        Debug.Log("Start button clicked - loading GameScene...");
        SceneManager.LoadScene("Battle1");
    }


    void OnLoadClicked()
    {
        Debug.Log("No Saved Game!");
        Debug.Log("Coming soon!");
    }

    void OnOptionsClicked()
    {
        Debug.Log("Options button clicked");
    }

    void OnQuitClicked()
    {
        Debug.Log("Quit button clicked");
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
