using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Enemy : MonoBehaviour
{
    public string enemyName;
    public int maxHP;
    public int currentHP;
    public int shield = 0;

    private EnemyUI enemyUI;
    protected Animator animator;
    protected TMP_Text intentText;

    protected readonly List<StatusType> activeStatuses = new();

    public virtual void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        intentText = GetComponentInChildren<TMP_Text>();
        enemyUI = GetComponentInChildren<EnemyUI>();

        if (enemyUI != null)
        {
            enemyUI.enemy = this; 
            enemyUI.UpdateUI();
            Debug.Log($"[Enemy] Linked EnemyUI for {enemyName}");
        }
    }


    public virtual void TakeDamage(int amount)
    {
        int remaining = amount;

        if (shield > 0)
        {
            int absorbed = Mathf.Min(shield, remaining);
            shield -= absorbed;
            remaining -= absorbed;
        }

        currentHP -= remaining;
        currentHP = Mathf.Max(currentHP, 0);

        Debug.Log($"{enemyName} took {amount} damage. Shield: {shield}, HP: {currentHP}");
        enemyUI?.UpdateUI();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public virtual void TakeUnblockableDamage(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Max(currentHP, 0);

        Debug.Log($"{enemyName} took {amount} unblockable damage. HP: {currentHP}");
        enemyUI?.UpdateUI();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void GainShield(int amount)
    {
        shield += amount;
        Debug.Log($"{enemyName} gained {amount} block. Total shield: {shield}");
        enemyUI?.UpdateUI();
    }

    public virtual void Die()
    {
        if (intentText != null)
            intentText.text = "";

        if (animator != null)
            animator.SetTrigger("Die");

        Debug.Log($"{enemyName} has died.");
        EnemyManager.Instance.RemoveEnemy(this);

        Destroy(gameObject, 1.5f);
    }

    public abstract void PrepareIntent();
    public abstract void PerformTurn();

    public bool HasDebuff()
    {
        return activeStatuses.Contains(StatusType.Debuffed) || activeStatuses.Contains(StatusType.WeakenAttack);
    }

    public void ApplyStatus(StatusType type, int duration = 1, int magnitude = 0)
    {
        if (!activeStatuses.Contains(type))
            activeStatuses.Add(type);

        Debug.Log($"{enemyName} received status: {type}");
        enemyUI?.UpdateUI();
    }
}
