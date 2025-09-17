using UnityEngine;

public class MothEnemy : Enemy
{
    private enum Action { Attack, Unblockable }
    private Action nextAction;

    public override void Start()
    {
        enemyName = "Moth";
        maxHP = 30;
        base.Start();
    }

    public override void PrepareIntent()
    {
        float roll = Random.value;
        nextAction = (roll < 0.2f) ? Action.Unblockable : Action.Attack;

        Debug.Log($"[Moth] PrepareIntent - Decided to {nextAction}");

        if (intentText != null)
        {
            intentText.text = nextAction == Action.Attack
                ? "Attack: 8"
                : "Piercing Screech (10 Unblockable)";
        }
    }

    public override void PerformTurn()
    {
        Debug.Log($"[Moth] PerformTurn - Action: {nextAction}");

        switch (nextAction)
        {
            case Action.Attack:
                Player.Instance.TakeDamage(8);
                Debug.Log("[Moth] Dealt 8 normal damage.");
                animator?.SetTrigger("Attack");
                break;

            case Action.Unblockable:
                Player.Instance.TakeUnblockableDamage(10);
                Debug.Log("[Moth] Dealt 10 unblockable damage.");
                animator?.SetTrigger("Attack");
                break;
        }

        if (intentText != null)
            intentText.text = "";
    }
}
