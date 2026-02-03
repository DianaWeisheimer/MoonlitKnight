using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CompanionStateKnightCombat : AIState
{
    public Enemy currentTarget;
    public float enemyFollowStoppingDistance;

    public float attackRange;
    public float attackCooldown;

    public override void OnStateEnter()
    {
        AttackLoop(true);
        brain.agent.stoppingDistance = enemyFollowStoppingDistance;
    }

    public override void Tick()
    {
        if (currentTarget)
        {
            brain.agent.SetDestination(currentTarget.transform.position);
        }

        else
        {
            CheckAggro();
        }
    }

    public void CheckAggro()
    {
        
    }

    private void AttackLoop(bool attack)
    {
        if (attack)
        {
            InvokeRepeating("CheckForAttack", 1f, .5f);
        }

        else
        {
            CancelInvoke();
        }
    }

    private void CheckForAttack()
    {
        if (currentTarget != null)
        {
            float distanceFromEnemy = Vector3.Distance(transform.position, currentTarget.transform.position);

            if (distanceFromEnemy < attackRange)
            {
                brain.combatActor.Attack();
            }
        }
    }

    public void PlayerLowHealth()
    {
        brain.combatActor.UseAbility("BraveryCall");
    }

    public override void OnStateExit()
    {
        AttackLoop(false);
    }
}
