using UnityEngine;

public class CollectRewardsUI : MonoBehaviour
{
    public GameObject doorChoicePanel; // Panel with the 3-door background
    public DoorIconRandomizer iconRandomizer;
    public GameObject sceneToHide;

    public void OnCollectRewardsClicked()
    {
        // Show the door selection screen
        doorChoicePanel.SetActive(true);

        // Optionally hide other UI elements
        sceneToHide.SetActive(false); // Hide this button

        iconRandomizer.RandomizeIcons();
    }
}