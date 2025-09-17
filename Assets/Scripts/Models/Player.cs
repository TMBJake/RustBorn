using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public int maxHealth = 100;
    public int currentHealth;
    public int shield = 0;
    public int mana = 3;

    public List<RelicData> relics = new List<RelicData>();
    public bool PreserveBlockThisTurn = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        currentHealth = maxHealth;
    }

    public void StartTurn()
    {
        Debug.Log("[Player] StartTurn called.");

        foreach (var relic in relics)
        {
            relic.OnTurnStart(this);
        }

        if (!PreserveBlockThisTurn)
        {
            shield = 0;
            Debug.Log("[Player] Shield reset to 0.");
        }

        PreserveBlockThisTurn = false;
        GainMana(3);
    }



    public void TakeDamage(int amount)
    {
        int remaining = amount;

        if (shield > 0)
        {
            int absorbed = Mathf.Min(shield, remaining);
            shield -= absorbed;
            remaining -= absorbed;
        }

        currentHealth -= remaining;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log($"Player took {amount} damage. Shield: {shield}, HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void GainShield(int amount)
    {
        shield += amount;
        Debug.Log($"Player gained {amount} shield. Total: {shield}");
    }

    public void GainMana(int amount)
    {
        mana += amount;
        Debug.Log($"Player gained {amount} mana. Total: {mana}");
    }

    public void UseMana(int amount)
    {
        mana = Mathf.Max(mana - amount, 0);
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log($"Player healed for {amount}. Current HP: {currentHealth}");
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        // Trigger game over screen or reload scene
    }
    public void TakeUnblockableDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log($"Player took {amount} unblockable damage. HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public int StealHalfShield()
    {
        int stolen = shield / 2;
        shield -= stolen;
        Debug.Log($"[Player] {stolen} shield stolen. Remaining shield: {shield}");
        return stolen;
    }

    public void ResetShield()
    {
        shield = 0;
        Debug.Log("[Player] Shield reset to 0.");
    }
}
