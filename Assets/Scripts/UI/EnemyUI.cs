using UnityEngine;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    public Enemy enemy;
    public TMP_Text healthText;
    public TMP_Text shieldText;
    public TMP_Text debuffText;

    void Start()
    {
        if (enemy == null)
            enemy = GetComponentInParent<Enemy>();

        if (enemy == null)
        {
            Debug.LogWarning("EnemyUI: Enemy reference is missing.");
            return;
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (enemy == null)
        {
            Debug.LogWarning("[EnemyUI] Missing enemy reference!");
            return;
        }

        if (healthText != null)
            healthText.text = $"HP: {enemy.currentHP} / {enemy.maxHP}";

        if (shieldText != null)
            shieldText.text = enemy.shield > 0 ? $"Shield: {enemy.shield}" : "";

        if (debuffText != null)
            debuffText.text = enemy.HasDebuff() ? "Debuffed!" : "";

        Debug.Log($"[EnemyUI] Updated: {enemy.enemyName} = {enemy.currentHP}/{enemy.maxHP}");
    }

}
