using UnityEngine;


// 10. Bonus reward effect (placeholder: more mana)
[CreateAssetMenu(menuName = "Relic/DataMiner")]
public class DataMiner : RelicData
{
    public override void OnTurnStart(Player player)
    {
        player.GainMana(1);
    }
}