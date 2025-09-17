using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class HandView : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;

    private readonly List<CardView> cards = new();

    public IEnumerator AddCard(CardView cardView)
    {
        cards.Add(cardView);

        float cardSpacing = 1f / 10f;
        float firstCardPosition = 0.5f - (cards.Count - 1) * cardSpacing / 2;
        float p = firstCardPosition + (cards.Count - 1) * cardSpacing;

        Spline spline = splineContainer.Spline;
        Vector3 splinePosition = spline.EvaluatePosition(p);
        Vector3 forward = spline.EvaluateTangent(p);
        Vector3 up = spline.EvaluateUpVector(p);

        Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);

        // Set spawn position to Z = 2
        Vector3 fixedStart = splinePosition + transform.position;
        fixedStart.z = 2f;
        cardView.transform.position = fixedStart;
        cardView.transform.rotation = rotation;

        yield return UpdateCardPositions(0.15f);
    }

    public CardView RemoveCard(Card card)
    {
        CardView cardView = GetCardView(card);
        if (cardView == null || cardView.gameObject == null)
        {
            Debug.LogWarning("Attempted to remove a null or destroyed CardView.");
            return null;
        }
        cards.Remove(cardView);
        StartCoroutine(UpdateCardPositions(0.15f));
        return cardView;
    }

    public void ForceRemoveCard(Card card)
    {
        CardView view = GetCardView(card);
        if (view != null)
        {
            cards.Remove(view);
        }
    }

    private CardView GetCardView(Card card)
    {
        return cards.FirstOrDefault(cardView => cardView.Card == card);
    }

    private IEnumerator UpdateCardPositions(float duration)
    {
        if (cards.Count == 0) yield break;

        float cardSpacing = 1f / 10f;
        float firstCardPosition = 0.5f - (cards.Count - 1) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;

        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] == null || cards[i].gameObject == null) continue;

            float p = firstCardPosition + i * cardSpacing;

            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);

            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);

            Vector3 finalPosition = splinePosition + transform.position;
            finalPosition.z = 2f + 0.01f * i;

            cards[i].transform.DOMove(finalPosition, duration);
            cards[i].transform.DORotate(rotation.eulerAngles, duration);
        }

        yield return new WaitForSeconds(duration);
    }
}