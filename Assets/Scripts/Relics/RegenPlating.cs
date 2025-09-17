using UnityEngine;


// 3. Heal 1 when gaining shield (simulated via OnTurnStart)
[CreateAssetMenu(menuName = "Relic/RegenPlating")]
public class RegenPlating : RelicData
{
    public override void OnTurnStart(Player player)
    {
        if (player.shield > 0)
            player.Heal(1);
    }
}

