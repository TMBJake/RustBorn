using UnityEngine;
using UnityEngine.UI;

public class DoorIconRandomizer : MonoBehaviour
{
    public Image[] doorIconSlots;
    public Sprite battleNormal, battleElite, rest, shop, mystery, treasure, eventIcon;

    private string[] chosenTypes = new string[3]; // Save for logic later

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Keep across scenes if needed
    }

    public void RandomizeIcons()
    {
        for (int i = 0; i < 3; i++)
        {
            int roll = Random.Range(1, 54); // 1 to 90 inclusive

            if (roll <= 50)
            {
                doorIconSlots[i].sprite = battleNormal;
                chosenTypes[i] = "Battle_Normal";
            }
            else if (roll <= 54)
            {
                doorIconSlots[i].sprite = battleElite;
                chosenTypes[i] = "Battle_Elite";
            }
            //else if (roll <= 57)
            //{
            //    doorIconSlots[i].sprite = rest;
            //    chosenTypes[i] = "Rest";
            //}
            //else if (roll <= 61)
            //{
            //    doorIconSlots[i].sprite = shop;
            //    chosenTypes[i] = "Shop";
            //}
            //else if (roll <= 81)
            //{
            //    doorIconSlots[i].sprite = mystery;
            //    chosenTypes[i] = "Mystery";
            //}
            //else if (roll <= 83)
            //{
            //    doorIconSlots[i].sprite = treasure;
            //    chosenTypes[i] = "Treasure";
            //}
            //else
            //{
            //    doorIconSlots[i].sprite = eventIcon;
            //    chosenTypes[i] = "Event";
            //}

            Debug.Log($"Door {i} is {chosenTypes[i]}");
        }
    }

    public string GetDoorType(int index)
    {
        if (index < 0 || index >= chosenTypes.Length)
        {
            Debug.LogError("Invalid door index: " + index);
            return "Unknown";
        }

        return chosenTypes[index];
    }
}
