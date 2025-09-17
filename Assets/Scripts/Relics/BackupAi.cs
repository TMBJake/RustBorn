using UnityEngine;

// 7. Hand empty = draw 2 cards (simulated via health check here for demo)
[CreateAssetMenu(menuName = "Relic/BackupAI")]
public class BackupAi : RelicData
{
    public override void OnTurnStart(Player player)
    {
        if (player.mana == 0)
            player.GainMana(1);
    }
}


