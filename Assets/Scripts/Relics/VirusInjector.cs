using UnityEngine;

// 9. Debuffed enemy = gain shield
[CreateAssetMenu(menuName = "Relic/VirusInjector")]
public class VirusInjector : RelicData
{
    public override void OnTurnStart(Player player)
    {
        player.GainShield(1);
    }
}