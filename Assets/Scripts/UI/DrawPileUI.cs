using UnityEngine;

public class DrawPileUI : MonoBehaviour
{
    public static DrawPileUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowSelectable()
    {
        Debug.Log("Showing draw pile for card selection.");
        // Open UI window to choose a card
    }
}
