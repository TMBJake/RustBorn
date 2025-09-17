using UnityEngine;

public class Interactions : Singleton<Interactions>
{

    public bool PlayerIsDragging { get; set; } = false;
    public CardView CurrentDraggedCard { get; set; } = null;


    public bool PlayerCanInteract()
    {
        if (!ActionSystem.Instance.IsPerforming) return true;
        else return false;
    }

    public bool PlayerCanHover()
    {
        if (PlayerIsDragging) return false;
        return true;
    }
}
