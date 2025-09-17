using UnityEngine;



// 8. Stand-in: grant 1 extra mana at start (reward expansion stub)
[CreateAssetMenu(menuName = "Relic/CommandAntenna")]
public class CommandAntenna : RelicData
{
    public override void OnTurnStart(Player player)
    {
        player.GainMana(1);
    }
}