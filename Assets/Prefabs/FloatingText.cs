using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float floatSpeed = 20f;
    public float fadeDuration = 1f;

    private TMP_Text text;
    private CanvasGroup canvasGroup;
    private float elapsed = 0f;

    public void Setup(string message)
    {
        text = GetComponent<TMP_Text>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup != null)
            canvasGroup.alpha = 1f;

        if (text != null)
            text.text = message;
    }

    void Update()
    {
        if (canvasGroup == null || text == null) return;

        // Move upward using unscaled time
        transform.position += Vector3.up * floatSpeed * Time.unscaledDeltaTime;

        // Fade out over time using unscaled time
        elapsed += Time.unscaledDeltaTime;
        canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);

        // Destroy after fade
        if (elapsed >= fadeDuration)
        {
            Destroy(gameObject);
        }
    }
}
