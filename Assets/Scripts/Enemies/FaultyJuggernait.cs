using UnityEngine;

public class FaultJuggernautEnemy : Enemy
{
    private enum Action { Attack, FortifySuccess, FortifyFail }
    private Action nextAction;
    private bool isFirstTurn = true;

    public override void Start()
    {
        enemyName = "Fault Juggernaut";
        maxHP = 60;
        base.Start();
    }

    public override void PrepareIntent()
    {
        if (isFirstTurn)
        {
            isFirstTurn = false;
            nextAction = Action.Attack;
            if (intentText != null) intentText.text = "Smash: 15 Damage";
            Debug.Log("[Juggernaut] First turn - guaranteed Attack.");
            return;
        }

        float roll = Random.value;

        if (roll < 0.35f)
        {
            // Tried to fortify
            float successRoll = Random.value;
            if (successRoll < 0.35f)
            {
                nextAction = Action.FortifySuccess;
                if (intentText != null) intentText.text = "Fortify: Gain 20 Shield";
            }
            else
            {
                nextAction = Action.FortifyFail;
                if (intentText != null) intentText.text = "Fortify Failed: Self Damage 5";
            }

            Debug.Log($"[Juggernaut] Attempting fortify. Roll: {roll:F2}, Result: {nextAction}");
        }
        else
        {
            nextAction = Action.Attack;
            if (intentText != null) intentText.text = "Smash: 15 Damage";
            Debug.Log($"[Juggernaut] Chose to Attack (Roll: {roll:F2})");
        }
    }

    public override void PerformTurn()
    {
        Debug.Log($"[Juggernaut] PerformTurn - Action: {nextAction}");

        switch (nextAction)
        {
            case Action.Attack:
                Player.Instance.TakeDamage(15);
                animator?.SetTrigger("Attack");
                Debug.Log("[Juggernaut] Attacked for 15 damage.");
                break;

            case Action.FortifySuccess:
                GainShield(20);
                animator?.SetTrigger("Defend");
                Debug.Log("[Juggernaut] Fortified successfully for 20 shield.");
                break;

            case Action.FortifyFail:
                TakeUnblockableDamage(5);
                animator?.SetTrigger("Stun");
                Debug.Log("[Juggernaut] Fortify failed. Took 5 self-damage.");
                break;
        }

        if (intentText != null)
            intentText.text = "";
    }
}
