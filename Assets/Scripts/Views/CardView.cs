using TMPro;
using UnityEngine;
using System.Collections;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text cost;
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private GameObject wrapper;
    [SerializeField] private LayerMask dropLayer;
    private bool isHovering = false;
    public Card Card { get; private set; }
    private Vector3 dragStartPosition;
    private Quaternion dragStartRotation;
    public void Setup(Card card)
    {
        Card = card;
        title.text = card.Title;
        description.text = card.Description;
        cost.text = card.Mana.ToString();
        image.sprite = card.Image;
    }

    //Positions and calls the show method when the mouse is on a card
    void OnMouseEnter()
    {
        if (isHovering) return;
        if (RestManager.isRested) return;
        if (EndLevelManager.isEnded) return;
        if (PauseManager.isPaused) return;
        if (Interactions.Instance.PlayerIsDragging) return;
        if (!Interactions.Instance.PlayerCanHover()) return;
        isHovering = true;
        //disable the wrapper so cardview isn't visible anymore
        wrapper.SetActive(false);
        // Raise the card 2 units above while not overriding original positions
        Vector3 pos = transform.position + new Vector3(0, 2, -1);
        CardViewHoverSystem.Instance.Show(Card, pos);
    }

    //LeShrink
    void OnMouseExit()
    {
        if (!Interactions.Instance.PlayerCanHover()) return;

        isHovering = false;
        CardViewHoverSystem.Instance.Hide();
        wrapper.SetActive(true);
    }

    private void OnMouseDown()
    {
        if (!Interactions.Instance.PlayerCanInteract()) return;
        Interactions.Instance.PlayerIsDragging = true;
        wrapper.SetActive(true);
        CardViewHoverSystem.Instance.Hide();
        dragStartPosition = transform.position;
        dragStartRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
    }

    private void OnMouseDrag()
    {
        if (!Interactions.Instance.PlayerCanInteract()) return;
        wrapper.SetActive(true);
        transform.position = MouseUtil.GetMousePositionInWorldSpace(5);
    }

    private void OnMouseUp()
    {
        if (!Interactions.Instance.PlayerCanInteract()) return;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (ManaSystem.Instance.HasEnoughMana(Card.Mana)
        && Physics2D.OverlapPoint(mousePos, dropLayer))

        {
            PlayCardGA playCardGA = new(Card);
            ActionSystem.Instance.Perform(playCardGA);
        }
        else
        {
            transform.position = dragStartPosition;
            transform.rotation = dragStartRotation;
        }

        Interactions.Instance.PlayerIsDragging = false;
    }

    void Start()
    {
        StartCoroutine(AssignCameraNextFrame());
    }

    private IEnumerator AssignCameraNextFrame()
    {
        yield return null; // wait one frame
        Canvas canvas = GetComponentInChildren<Canvas>();
        if (canvas != null && canvas.renderMode == RenderMode.WorldSpace)
        {
            canvas.worldCamera = Camera.main;
        }
    }
    public void ResetHover()
    {
        isHovering = false;
        CardViewHoverSystem.Instance.Hide();
        wrapper.SetActive(true);
    }


}
