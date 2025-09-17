using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCollectionManager : MonoBehaviour
{
    [SerializeField] private GameObject cardViewPrefab;
    [SerializeField] private Transform cardDisplayParent;
    [SerializeField] private Button collectCardButton;
    [SerializeField] private HandView handView;
    [SerializeField] private Transform spawnParent;

    private List<CardView> currentCardViews = new();
    private CardView selectedCard;

    public List<Card> cardPool; 

    void Start()
    {
        collectCardButton.onClick.AddListener(CollectSelectedCard);
    }

    public void ShowCardChoices()
    {
        ClearCards();

        for (int i = 0; i < 3; i++)
        {
            Card randomCard = cardPool[Random.Range(0, cardPool.Count)];

            GameObject cardObj = Instantiate(cardViewPrefab, cardDisplayParent);
            CardView cardView = cardObj.GetComponent<CardView>();
            cardView.Setup(randomCard);
            currentCardViews.Add(cardView);

            // Add click handler
            Button cardButton = cardObj.GetComponent<Button>();
            if (cardButton != null)
            {
                int index = i; // capture the current index
                cardButton.onClick.AddListener(() => OnCardSelected(cardView));
            }
        }

        selectedCard = null;
    }

    private void OnCardSelected(CardView cardView)
    {
        // Deselect all
        foreach (var view in currentCardViews)
            view.GetComponent<Image>().color = Color.white;

        // Select current
        selectedCard = cardView;
        selectedCard.GetComponent<Image>().color = Color.green;
    }

    private void CollectSelectedCard()
    {
        if (selectedCard == null) return;

        // Instantiate a CardView from the prefab
        GameObject cardObj = Instantiate(cardViewPrefab, spawnParent); // spawnParent is optional
        CardView newCardView = cardObj.GetComponent<CardView>();

        // Set the card data
        newCardView.Setup(selectedCard.Card);

        // Add to hand using coroutine
        StartCoroutine(handView.AddCard(newCardView));

        Debug.Log("Card added to hand: " + selectedCard.Card.Title);

        ClearCards(); // Hide the reward choices
    }

    private void ClearCards()
    {
        foreach (var view in currentCardViews)
            Destroy(view.gameObject);
        currentCardViews.Clear();
    }
}
