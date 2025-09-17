using UnityEngine;

public abstract class PassiveEffect : Effect
{
    public override GameAction GetGameAction() => null;

    public virtual void OnGainMana(Player player, int amount) { }
}