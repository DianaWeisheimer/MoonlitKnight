using UnityEngine;
using System.Collections.Generic;

public class CompanionBrainKnight : AIBrain
{
    public CompanionStateFollowPlayer stateFollowPlayer;
    public CompanionStateKnightCombat stateCombat;
    public Enemy currentTarget;
    private bool playerLowHealth = false;

    private void Start()
    {
        stateFollowPlayer.stoppingDistance = 4;
    }

    private void Update()
    {
        currentState.Tick();
        ChooseTarget();
        CheckPlayerHealth();
    }

    private void CheckPlayerHealth()
    {
        Character playerCharacter = PartyManager.instance.GetActiveMember().core.character;

        if (playerCharacter.stats.stats[StatType.Health].currentValue < playerCharacter.stats.stats[StatType.Health].maxValue * 0.3f)
        {
            stateCombat.PlayerLowHealth();
            playerLowHealth = true;
        }

        else
        {
            playerLowHealth = false;
        }
    }

    private void ChooseTarget()
    {
        var hostiles = EnemyDetectionSystem.Instance.GetHostileEnemies();
        if (hostiles.Count > 0)
        {
            // Pick closest hostile
            if (playerLowHealth) currentTarget = GetHighestPlayerThreat();
            else currentTarget = GetHighestLevelEnemy(hostiles);

            stateCombat.currentTarget = currentTarget;
            ChangeState(stateCombat);
        }

        else
        {
            currentTarget = null;
        }
    }

    private Enemy GetHighestLevelEnemy(List<Enemy> enemies)
    {
        Enemy highest = null;
        float highestLevel = -1;

        foreach (var enemy in enemies)
        {
            if (highestLevel < enemy.character.stats.GetLevel())
            {
                highestLevel = enemy.character.stats.GetLevel();
                highest = enemy;
            }
        }

        return highest;
    }
    private Enemy GetHighestPlayerThreat()
    {
        Enemy highest = null;

        AggroTable table = PartyManager.instance.GetActiveMember().core.character.aggroTable;

        var entry = table.GetHighestAggro();
        highest = entry.character.GetComponent<Enemy>();

        return highest;
    }
    private Enemy GetHighestAggroEnemy(List<Enemy> enemies)
    {
        Enemy highest = null;
        float highestAggro = 0;

        foreach (var enemy in enemies)
        {
            if(highestAggro < enemy.character.aggroTable.GetHighestAggro().aggroValue)
            {
                highestAggro = enemy.character.aggroTable.GetHighestAggro().aggroValue;
                highest = enemy;
            }
        }

        return highest;
    }

    private Enemy GetClosestEnemy(List<Enemy> enemies)
    {
        Enemy closest = null;
        float closestDist = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = enemy;
            }
        }

        return closest;
    }
}
