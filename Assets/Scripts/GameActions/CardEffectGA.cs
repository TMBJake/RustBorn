using UnityEngine;

public class GainShieldGA : GameAction
{
    public int Amount { get; }
    public GainShieldGA(int amount) { Amount = amount; }
}

public class DealDamageGA : GameAction
{
    public int Amount { get; }
    public DealDamageGA(int amount) { Amount = amount; }
}

public class DealUnblockableDamageGA : GameAction
{
    public int Amount { get; }
    public DealUnblockableDamageGA(int amount) { Amount = amount; }
}

public class DamageRandomEnemiesGA : GameAction
{
    public int Damage { get; }
    public int Count { get; }
    public DamageRandomEnemiesGA(int damage, int count)
    {
        Damage = damage;
        Count = count;
    }
}

public class DrawCardsGA : GameAction
{
    public int Amount { get; set; }
    public DrawCardsGA(int amount) { Amount = amount; }
}

public class SelfDamageGA : GameAction
{
    public int Amount { get; }
    public SelfDamageGA(int amount) { Amount = amount; }
}

public class GainManaGA : GameAction
{
    public int Amount { get; }
    public GainManaGA(int amount) { Amount = amount; }
}

public class HealGA : GameAction
{
    public int Amount { get; }
    public HealGA(int amount) { Amount = amount; }
}

public class RemoveAllDebuffsGA : GameAction { }

public class ApplyStatusGA : GameAction
{
    public StatusType Status { get; }
    public int Duration { get; }
    public int Magnitude { get; }
    public ApplyStatusGA(StatusType status, int duration, int magnitude)
    {
        Status = status;
        Duration = duration;
        Magnitude = magnitude;
    }
}

public class ConditionalDamageGA : GameAction
{
    public int BaseDamage { get; }
    public int BonusDamage { get; }
    public System.Func<Enemy, bool> Condition { get; }
    public ConditionalDamageGA(int baseDamage, int bonusDamage, System.Func<Enemy, bool> condition)
    {
        BaseDamage = baseDamage;
        BonusDamage = bonusDamage;
        Condition = condition;
    }
}

public class ChooseCardFromDrawPileToPlayGA : GameAction { }

public class CompositeGA : GameAction
{
    public GameAction[] Actions { get; }
    public CompositeGA(params GameAction[] actions)
    {
        Actions = actions;
    }
}

public class DiscardRandomGA : GameAction
{
    public int Amount { get; }
    public DiscardRandomGA(int amount) { Amount = amount; }
}

public class DamageAllEnemiesGA : GameAction
{
    public int Amount { get; }
    public DamageAllEnemiesGA(int amount) { Amount = amount; }
}

public static class StatusConditions
{
    public static bool TargetIsDebuffed(Enemy e) => e.HasDebuff();
}

public enum StatusType
{
    Regen,
    WeakenAttack,
    Debuffed
}
