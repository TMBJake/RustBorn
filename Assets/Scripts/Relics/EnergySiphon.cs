using UnityEngine;

// 5. Simulate gaining mana if HP is below threshold
[CreateAssetMenu(menuName = "Relic/EnergySiphon")]
public class EnergySiphon : RelicData
{
    public override void OnTurnStart(Player player)
    {
        if (player.currentHealth < player.maxHealth / 2)
            player.GainMana(1);
    }
}