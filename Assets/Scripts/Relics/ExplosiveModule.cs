using UnityEngine;

// 14. Every 5th attack = splash (simulated shield)
[CreateAssetMenu(menuName = "Relic/ExplosiveModule")]
public class ExplosiveModule : RelicData
{
    public override void OnTurnStart(Player player)
    {
        player.GainShield(1);
    }
}