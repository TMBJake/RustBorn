using UnityEngine;

public class OilSpillEnemy : Enemy
{
    private enum Action { MegaBlast, ShieldAndHit }
    private Action nextAction;
    private bool hasActed = false;

    public override void Start()
    {
        enemyName = "Oil Spill";
        maxHP = 15;
        base.Start();

        // Force first move to be safe
        nextAction = Action.ShieldAndHit;

        if (intentText != null)
            intentText.text = "Shield + Hit: 6 DMG + 4 Shield";
    }

    public override void PrepareIntent()
    {
        if (!hasActed)
        {
            Debug.Log("[OilSpill] First turn - using predefined ShieldAndHit.");
            return; // Do not reassign — already set in Start()
        }

        float roll = UnityEngine.Random.Range(0f, 1f);
        nextAction = roll < 0.01f ? Action.MegaBlast : Action.ShieldAndHit;
        Debug.Log($"[OilSpill] PrepareIntent called. Random roll = {roll:F4}");

        if (intentText != null)
        {
            intentText.text = nextAction switch
            {
                Action.MegaBlast => "OVERPRESSURE! 75 DMG",
                Action.ShieldAndHit => "Shield + Hit: 6 DMG + 4 Shield",
                _ => ""
            };
        }
    }

    public override void PerformTurn()
    {
        Debug.Log($"[OilSpill] PerformTurn - Executing: {nextAction}");
        hasActed = true;

        switch (nextAction)
        {
            case Action.MegaBlast:
                Player.Instance.TakeUnblockableDamage(75);
                Debug.Log("[OilSpill] Used MegaBlast for 75 unblockable damage.");
                animator?.SetTrigger("Attack");
                break;

            case Action.ShieldAndHit:
                GainShield(4);
                Player.Instance.TakeDamage(6);
                Debug.Log("[OilSpill] Gained 4 shield and dealt 6 damage.");
                animator?.SetTrigger("Attack");
                break;
        }

        if (intentText != null)
            intentText.text = "";
    }
}
