using UnityEngine;


// 11. Simulate copied buff with mana
[CreateAssetMenu(menuName = "Relic/MirrorProtocol")]
public class MirrorProtocol : RelicData
{
    public override void OnTurnStart(Player player)
    {
        player.GainShield(2);
    }
}
