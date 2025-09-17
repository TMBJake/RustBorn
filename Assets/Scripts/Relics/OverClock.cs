using UnityEngine;


// 1. Gain +1 mana every turn
[CreateAssetMenu(menuName = "Relic/OverclockCore")]
public class OverclockCore : RelicData
{
    public override void OnTurnStart(Player player)
    {
        player.GainMana(1);
    }
}