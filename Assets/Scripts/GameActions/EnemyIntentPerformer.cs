using System.Collections;
using UnityEngine;

public static class EnemyIntentPerformer
{
    public static IEnumerator PerformIntentPrep(EnemyPrepareIntentGA action)
    {
        Debug.Log("[EnemyIntentPerformer] Starting intent preparation...");

        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                Debug.Log($"[EnemyIntentPerformer] Calling PrepareIntent on: {enemy.enemyName}");
                enemy.PrepareIntent();
            }
        }

        yield return null;
    }
}
