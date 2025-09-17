using UnityEngine;


// 15. Extra skill per battle (simulated with turn-start mana bonus)
[CreateAssetMenu(menuName = "Relic/CircuitExtender")]
public class CircuitExtender : RelicData
{
    public override void OnTurnStart(Player player)
    {
        player.GainMana(1);
    }
}


