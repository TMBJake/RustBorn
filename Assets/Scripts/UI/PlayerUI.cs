using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text shieldText;

    private void Update()
    {
        if (Player.Instance == null) return;

        healthText.text = $"HP: {Player.Instance.currentHealth} / {Player.Instance.maxHealth}";
        shieldText.text = Player.Instance.shield > 0 ? $"{Player.Instance.shield} Block" : "";
    }
}
