using UnityEngine;

public class MagnetHeadEnemy : Enemy
{
    private enum Action { StealShield, Attack }
    private Action nextAction;

    public override void Start()
    {
        enemyName = "Magnet Head";
        maxHP = 45;
        base.Start();
    }

    public override void PrepareIntent()
    {
        // Ensure conditions are logged and forced
        bool shouldSteal = Player.Instance.shield > 14 && Random.value < 0.5f;
        nextAction = shouldSteal ? Action.StealShield : Action.Attack;

        Debug.Log($"[MagnetHead] PrepareIntent - Decided to {(nextAction == Action.StealShield ? "steal" : "attack")} (Player shield: {Player.Instance.shield})");

        if (intentText != null)
        {
            intentText.text = nextAction == Action.StealShield
                ? "Steal Shield"
                : "Attack: 10";
        }
    }

    public override void PerformTurn()
    {
        Debug.Log($"[MagnetHead] PerformTurn - Action: {nextAction}");

        switch (nextAction)
        {
            case Action.StealShield:
                if (Player.Instance.shield <= 14)
                {
                    Debug.LogWarning("[MagnetHead] Trying to steal with insufficient player shield. Forcing fallback.");
                    nextAction = Action.Attack; // fallback safety
                    goto case Action.Attack;
                }

                int stolen = Player.Instance.StealHalfShield();
                this.GainShield(stolen);
                Debug.Log($"[MagnetHead] Stole {stolen} shield.");
                animator?.SetTrigger("Steal");
                break;

            case Action.Attack:
                Player.Instance.TakeDamage(10);
                Debug.Log("[MagnetHead] Attacked player for 10.");
                animator?.SetTrigger("Attack");
                break;
        }

        if (intentText != null)
            intentText.text = "";
    }
}
