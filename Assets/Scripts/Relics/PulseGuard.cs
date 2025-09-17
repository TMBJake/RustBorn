using UnityEngine;


// 4. +3 shield every turn (to simulate PulseGuard-like behavior)
[CreateAssetMenu(menuName = "Relic/PulseGuard")]
public class PulseGuard : RelicData
{
    public override void OnTurnStart(Player player)
    {
        player.GainShield(3);
    }
}
