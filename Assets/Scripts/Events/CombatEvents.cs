using System;
public class CombatEvents
{
    public event Action<Enemy> OnEnemyAggroPlayer;
    public void EnemyAggroPlayer(Enemy enemy)
    {
        if (OnEnemyAggroPlayer != null)
        {
            OnEnemyAggroPlayer(enemy);
        }
    }

    public event Action<Enemy> OnEnemyLosePlayer;
    public void EnemyLosePlayer(Enemy enemy)
    {
        if (OnEnemyLosePlayer != null)
        {
            OnEnemyLosePlayer(enemy);
        }
    }
    public event Action onPlayerDied;
    public void PlayerDied()
    {
        if (onPlayerDied != null)
        {
            onPlayerDied?.Invoke();
        }
    }

    public event Action onPlayerAttack;
    public void PlayerAttack()
    {
        if (onPlayerAttack != null)
        {
            onPlayerAttack?.Invoke();
        }
    }
}
