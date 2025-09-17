using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance != null)
        {
            SceneManager.LoadScene(GameManager.Instance.nextScene);
        }
        else
        {
            Debug.LogError("GameManager is missing. Cannot load target scene.");
        }
    }
}
