using UnityEngine;

public class TinkererEnemy : Enemy
{
    private enum Action { Repair, Attack }
    private Action nextAction;
    private bool hasActed = false;

    public override void Start()
    {
        enemyName = "Tinkerer";
        maxHP = 30;
        base.Start();

        // First turn always Attack
        nextAction = Action.Attack;
        if (intentText != null)
            intentText.text = "Attack: 7 damage";
    }

    public override void PrepareIntent()
    {
        if (!hasActed)
        {
            Debug.Log("[Tinkerer] First turn - using Attack.");
            return;
        }

        // Always heal if HP is 8 or below
        nextAction = currentHP <= 8 ? Action.Repair : Action.Attack;

        Debug.Log($"[Tinkerer] PrepareIntent: HP = {currentHP}, nextAction = {nextAction}");

        if (intentText != null)
        {
            intentText.text = nextAction switch
            {
                Action.Repair => "Self-Repair!",
                Action.Attack => "Attack: 7 damage",
                _ => ""
            };
        }
    }

    public override void PerformTurn()
    {
        Debug.Log($"[Tinkerer] PerformTurn - Action: {nextAction}");
        hasActed = true;

        switch (nextAction)
        {
            case Action.Repair:
                currentHP = maxHP;
                Debug.Log("[Tinkerer] Repaired to full HP.");
                animator?.SetTrigger("Heal");
                GetComponentInChildren<EnemyUI>()?.UpdateUI();
                break;

            case Action.Attack:
                Player.Instance.TakeDamage(7);
                Debug.Log("[Tinkerer] Dealt 7 damage to player.");
                animator?.SetTrigger("Attack");
                break;
        }

        if (intentText != null)
            intentText.text = "";
    }
}
