using UnityEngine;


// 13. First card duplicated (not replicable here) simulated with extra shield
[CreateAssetMenu(menuName = "Relic/TemporalLoop")]
public class TemporalLoop : RelicData
{
    public override void OnTurnStart(Player player)
    {
        player.GainShield(4);
    }
}