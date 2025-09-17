using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
    [SerializeField] private HandView handView;
    [SerializeField] private Transform drawPilePoint;
    [SerializeField] private Transform discardPilePoint;

    private readonly List<Card> drawPile = new();
    private readonly List<Card> discardPile = new();
    private readonly List<Card> hand = new();

    void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DiscardAllCardsGA>(DiscardAllCardsPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    void OnDisable()
    {
        ActionSystem.DetatchPerformer<DrawCardsGA>();
        ActionSystem.DetatchPerformer<DiscardAllCardsGA>();
        ActionSystem.DetatchPerformer<PlayCardGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    public void Setup(List<CardData> deckData)
    {
        drawPile.Clear();
        discardPile.Clear();
        hand.Clear();

        foreach (var cardData in deckData)
        {
            Card card = new(cardData);
            drawPile.Add(card);
        }

        Shuffle(drawPile);
    }

    private IEnumerator DrawCardsPerformer(DrawCardsGA drawCardsGA)
    {
        for (int i = 0; i < drawCardsGA.Amount; i++)
        {
            yield return DrawCard();
        }
    }

    private IEnumerator DiscardAllCardsPerformer(DiscardAllCardsGA discardAllCardsGA)
    {
        foreach (var card in hand)
        {
            discardPile.Add(card);
            CardView cardView = handView.RemoveCard(card);
            if (cardView != null && cardView.gameObject != null)
            {
                yield return DiscardCard(cardView);
            }
            else
            {
                Debug.LogWarning($"[Discard] Skipping null or destroyed card: {card.Title}");
            }
        }
        hand.Clear();
    }

    private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    {
        hand.Remove(playCardGA.card);
        discardPile.Add(playCardGA.card);

        CardView cardView = handView.RemoveCard(playCardGA.card);
        yield return DiscardCard(cardView); // Destroy visual only

        ActionSystem.Instance.AddReaction(new SpendManaGA(playCardGA.card.Mana));

        foreach (var effect in playCardGA.card.Effects)
        {
            ActionSystem.Instance.AddReaction(new PerformEffectGA(effect));
        }
    }

    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
        ActionSystem.Instance.AddReaction(new DiscardAllCardsGA());
    }

    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        ActionSystem.Instance.AddReaction(new DrawCardsGA(5));
    }

    private IEnumerator DrawCard()
    {
        // Reshuffle if needed
        if (drawPile.Count == 0)
        {
            if (discardPile.Count == 0)
            {
                Debug.LogWarning("[CardSystem] No cards left to draw or reshuffle.");
                yield break;
            }

            Debug.Log("[CardSystem] Reshuffling discard pile into draw pile.");
            RefillDeck();
        }

        Card card = drawPile[0];
        drawPile.RemoveAt(0);

        if (card == null)
        {
            Debug.LogError("[CardSystem] Tried to draw a null card.");
            yield break;
        }

        hand.Add(card);
        CardView cardView = CardViewCreator.Instance.CreateCardView(card, drawPilePoint.position, drawPilePoint.rotation);
        if (cardView == null)
        {
            Debug.LogError("[CardSystem] Failed to create CardView.");
            yield break;
        }

        yield return handView.AddCard(cardView);
    }

    private void RefillDeck()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear();
        Shuffle(drawPile);
    }

    private void Shuffle(List<Card> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }

    private IEnumerator DiscardCard(CardView cardView)
    {
        if (cardView == null || cardView.gameObject == null)
        {
            yield break;
        }

        cardView.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardView.transform.DOMove(discardPilePoint.position, 0.15f);
        yield return tween.WaitForCompletion();

        if (cardView != null && cardView.gameObject != null)
        {
            Destroy(cardView.gameObject);
        }
    }

    public IEnumerator PerformDraw(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            yield return DrawCard();
        }
    }

    public IEnumerator PerformRandomDiscards(int amount)
    {
        for (int i = 0; i < amount && hand.Count > 0; i++)
        {
            int index = Random.Range(0, hand.Count);
            var card = hand[index];
            hand.RemoveAt(index);
            discardPile.Add(card);

            CardView view = handView.RemoveCard(card);
            if (view != null)
            {
                yield return DiscardCard(view);
            }
        }
    }
}
