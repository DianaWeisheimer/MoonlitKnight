using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField] private List<EnemyCore> enemies = new();

    private void Awake()
    {
        instance = this;
    }

    public void RegisterEnemy(EnemyCore enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    public void ResetEnemies()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
                enemy.GetComponent<Enemy>().ResetEnemy();
        }
    }
}

