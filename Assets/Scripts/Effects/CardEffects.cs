using UnityEngine;
public class AdaptEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new CompositeGA(
            new DrawCardsGA(2),
            new DiscardRandomGA(1)
        );
    }
}

public class BatteryLoopEffect : PassiveEffect
{
    public override void OnGainMana(Player player, int amount)
    {
        player.GainMana(1);
    }
}

public class BurstBeamEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new DamageRandomEnemiesGA(6, 2);
    }
}

public class CoolantFlushEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new CompositeGA(
            new RemoveAllDebuffsGA(),
            new DrawCardsGA(1)
        );
    }
}

public class DefensiveJabEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new CompositeGA(
            new DealDamageGA(3),
            new GainShieldGA(2)
        );
    }
}

public class DefensiveUnitEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new GainShieldGA(20);
    }
}
public class DivertPowerEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new CompositeGA(
            new GainManaGA(2),
            new GainShieldGA(14),
            new ApplyStatusGA(StatusType.WeakenAttack, 1, -6)
        );
    }
}


public class GainManaNextAttackDebuffEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new CompositeGA(
            new GainManaGA(2),
            new ApplyStatusGA(StatusType.WeakenAttack, 1, -6)
        );
    }
}

public class DrawBoostEffect : PowerEffect
{
    public override void OnTurnStart(Player player)
    {
        ActionSystem.Instance.AddReaction(new DrawCardsGA(1));
    }
}

public class JoltEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new CompositeGA(
            new DealDamageGA(3),
            new GainShieldGA(1)
        );
    }
}

public class LaserEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new DealDamageGA(4);
    }
}

public class NewMetalEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new GainShieldGA(5);
    }
}

public class OverclockEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new CompositeGA(
            new SelfDamageGA(5),
            new GainManaGA(2)
        );
    }
}

public class OverdriveEffect : PowerEffect
{
    public override void OnAttack(Player player, ref int damage)
    {
        damage += 1;
    }
}

public class OverheatSlashEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new CompositeGA(
            new DealDamageGA(12),
            new SelfDamageGA(3)
        );
    }
}

public class PiercingRayEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new DealUnblockableDamageGA(8);
    }
}

public class PlasmaShotEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new CompositeGA(
            new DealDamageGA(12),
            new SelfDamageGA(3)
        );
    }
}

public class PowerConduitEffect : PowerEffect
{
    public override void OnTurnStart(Player player)
    {
        player.GainMana(1);
    }
}

public class PreloadEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new ChooseCardFromDrawPileToPlayGA();
    }
}

public class RegenCircuitEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new ApplyStatusGA(StatusType.Regen, 1, 10);
    }
}

public class SelfRepairEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new HealGA(10);
    }
}

public class ShieldMatrixEffect : PowerEffect
{
    public override void OnTurnStart(Player player)
    {
        player.GainShield(5);
    }
}

public class SkillfulCircuitryEffect : PowerEffect
{
    public override void OnCardCost(Player player, Card card, ref int cost)
    {
        if (card.Type == CardType.Skill)
        {
            cost = Mathf.Max(0, cost - 1);
        }
    }
}

public class SparkStrikeEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new ConditionalDamageGA(5, 10, StatusConditions.TargetIsDebuffed);
    }
}

public class VoltageArcEffect : Effect
{
    public override GameAction GetGameAction()
    {
        return new DamageAllEnemiesGA(5);
    }
}

public class DrawCardsEffect : Effect
{
    [SerializeField] private int drawAmount;
    public override GameAction GetGameAction()
    {
        return new DrawCardsGA(drawAmount);
    }
}
