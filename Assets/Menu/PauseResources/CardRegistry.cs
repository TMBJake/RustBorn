using System.Collections.Generic;
using UnityEngine;

public class CardRegistry : MonoBehaviour
{
    public static CardRegistry Instance { get; private set; }

    private readonly List<CardView> cards = new();

    private void Awake()
    {
        Instance = this;
    }

    public void Register(CardView card)
    {
        if (!cards.Contains(card)) cards.Add(card);
    }

    public void Unregister(CardView card)
    {
        if (cards.Contains(card)) cards.Remove(card);
    }

    public void ResetAllHovers()
    {
        // Create a copy to avoid collection modification errors during iteration
        var snapshot = new List<CardView>(cards);
        foreach (var card in snapshot)
        {
            card.ResetHover();
        }
    }

}
