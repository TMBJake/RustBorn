using DG.Tweening;
using UnityEngine;

public class CardViewCreator : Singleton<CardViewCreator>
{
    [SerializeField] private CardView cardViewPrefab;

    public CardView CreateCardView(Card card,Vector3 position, Quaternion rotation, Transform parent = null)
    {
        // Instantiate under parent if provided
        CardView cardView = Instantiate(cardViewPrefab, position, rotation, parent);

        // Reset local transform to avoid weird offset
        cardView.transform.localPosition = Vector3.zero;
        cardView.transform.localRotation = Quaternion.identity;
        cardView.transform.localScale = Vector3.zero;

        // Animate in
        cardView.transform.DOScale(Vector3.one, 0.15f);
        cardView.Setup(card);
        return cardView;
    }
}
