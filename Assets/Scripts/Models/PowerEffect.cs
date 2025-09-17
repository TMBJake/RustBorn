using UnityEngine;

[System.Serializable]
public abstract class PowerEffect : PassiveEffect
{
    public virtual void OnTurnStart(Player player) { }
    public virtual void OnAttack(Player player, ref int damage) { }
    public virtual void OnCardCost(Player player, Card card, ref int cost) { }
}
