using UnityEngine;


[CreateAssetMenu(menuName = "Relic/KeepBlockRelic")]
public class ToughMetal : RelicData
{
    public override void OnTurnStart(Player player)
    {
        player.PreserveBlockThisTurn = true;
    }
}