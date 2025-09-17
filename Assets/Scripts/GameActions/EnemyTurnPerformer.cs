using System.Collections;
using UnityEngine;

public static class EnemyTurnPerformer
{
    public static IEnumerator PerformEnemyTurn(EnemyTurnGA action)
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.PerformTurn();
                yield return new WaitForSeconds(0.6f); // delay between enemies for pacing
            }
        }

        yield return null;
    }
}
