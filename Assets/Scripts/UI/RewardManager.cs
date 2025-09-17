using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;

    public GameObject doorChoicePanel;
    public DoorIconRandomizer iconRandomizer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
