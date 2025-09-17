using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private List<Enemy> enemies = new();

    private void Awake()
    {
        Instance = this;
        enemies.Clear();
        enemies.AddRange(FindObjectsOfType<Enemy>());
    }

    public void DealDamageToDefaultTarget(int amount)
    {
        if (enemies.Count > 0)
            enemies[0].TakeDamage(amount);
    }

    public void DealUnblockableToDefaultTarget(int amount)
    {
        if (enemies.Count > 0)
            enemies[0].TakeUnblockableDamage(amount);
    }
    public bool AllEnemiesDead()
    {
        return enemies.Count == 0;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy))
            enemies.Remove(enemy);
    }

    public void DamageAllEnemies(int amount)
    {
        foreach (var e in enemies)
            e.TakeDamage(amount);
    }

    public void DamageRandomEnemies(int amount, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (enemies.Count == 0) break;
            int index = Random.Range(0, enemies.Count);
            enemies[index].TakeDamage(amount);
        }
    }
}
