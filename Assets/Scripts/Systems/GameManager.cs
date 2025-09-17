using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string nextScene;

    // Player state
    public int playerCoins = 0;
    public int playerHealth = 100;
    public List<string> relics = new List<string>();
    public List<string> deck = new List<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Utility methods
    public void AddCoins(int amount)
    {
        playerCoins += amount;
    }

    public void AddRelic(string relicId)
    {
        if (!relics.Contains(relicId))
            relics.Add(relicId);
    }

    public void AdjustHealth(int amount)
    {
        playerHealth = Mathf.Clamp(playerHealth + amount, 0, 100); // Change 100 to max HP if needed
    }
}
