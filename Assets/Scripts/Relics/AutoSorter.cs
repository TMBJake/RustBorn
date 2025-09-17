using UnityEngine;


// 12. First card free simulated: more mana
[CreateAssetMenu(menuName = "Relic/AutoSorter")]
public class AutoSorter : RelicData
{
    public override void OnTurnStart(Player player)
    {
        player.GainMana(1);
    }
}