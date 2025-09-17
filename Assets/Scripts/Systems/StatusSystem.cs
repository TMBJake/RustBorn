using UnityEngine;

public class StatusSystem : MonoBehaviour
{
    public static StatusSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void RemoveAllDebuffs(Player player)
    {
        Debug.Log("All debuffs removed from player.");
        // Actual implementation depends on how debuffs are stored
    }

    public void Apply(Player player, StatusType type, int duration, int magnitude)
    {
        Debug.Log($"Applied {type} to player for {duration} turns with {magnitude} magnitude.");
        // Store status on player or call a method there
    }
}

