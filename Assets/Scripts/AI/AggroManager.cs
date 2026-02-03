using System.Collections.Generic;
using UnityEngine;

public class AggroManager : MonoBehaviour
{
    public static AggroManager Instance { get; private set; }
    public List<Enemy> hostileEnemies = new List<Enemy>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddHostileEnemy(Enemy enemy)
    {
        if (!hostileEnemies.Contains(enemy))
            hostileEnemies.Add(enemy);
    }

    public void RemoveHostileEnemy(Enemy enemy)
    {
        if (hostileEnemies.Contains(enemy))
            hostileEnemies.Remove(enemy);
    }

    public bool IsEnemyHostile(Enemy enemy)
    {
        return hostileEnemies.Contains(enemy);
    }

    public List<Enemy> GetAllHostileEnemies()
    {
        return new List<Enemy>(hostileEnemies);
    }
}
