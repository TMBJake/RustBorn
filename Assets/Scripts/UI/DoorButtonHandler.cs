using UnityEngine.SceneManagement;
using UnityEngine;

public class DoorButtonHandler : MonoBehaviour
{
    public int doorIndex; // 0, 1, or 2
    public DoorIconRandomizer iconRandomizer;
    public GameObject doorPanel; 

    public void OnDoorClicked()
    {
        if (iconRandomizer == null)
        {
            Debug.LogError("DoorIconRandomizer is not assigned.");
            return;
        }

        string type = iconRandomizer.GetDoorType(doorIndex);
        Debug.Log($"Clicked door index: {doorIndex} is {type}");

        string sceneToLoad = "Game"; // fallback

        switch (type)
        {
            case "Battle_Normal":
                sceneToLoad = "Battle" + Random.Range(1, 5);
                break;
            case "Battle_Elite":
                sceneToLoad = "MiniBoss";
                break;
            case "Rest":
                sceneToLoad = "Rest";
                break;
            case "Shop":
                sceneToLoad = "Shop";
                break;
            case "Mystery":
                sceneToLoad = "Mystery";
                break;
            case "Treasure":
                sceneToLoad = "Treasure";
                break;
            case "Event":
                sceneToLoad = "Event";
                break;
            default:
                Debug.LogWarning("Unknown door type: " + type);
                break;
        }

        if (doorPanel != null)
        {
            doorPanel.SetActive(false);
        }

        if (Application.CanStreamedLevelBeLoaded(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError($"Scene '{sceneToLoad}' not found in Build Settings.");
        }
    }
}
