using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CompanionStateMeleeCombat : AIState
{
    public List<Enemy> enemies;
    public Enemy nearestEnemy;
    public float enemyDetectRadius;
    public float enemyFollowStoppingDistance;
    public event Action<bool> EnemyDetected;

    public float attackRange;
    public float attackCooldown;
    private bool attackable;

    private float _nearestDistance;
    private float _distance;

    private void Start()
    {
        attackable = true;
    }

    public override void Tick()
    {
        if (nearestEnemy)
        {
            brain.agent.SetDestination(nearestEnemy.transform.position);
        }
    }

    public override void OnStateEnter()
    {
        AttackLoop(true);
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
        if (nearestEnemy != null)
        {
            float distanceFromEnemy = Vector3.Distance(transform.position, nearestEnemy.transform.position);

            if (distanceFromEnemy < attackRange)
            {
                Attack();
            }
        }

        else
        {
            CheckNearbyEnemies();
        }
    }

    public void Attack()
    {
        if (attackable)
        {
            attackable = false;
            //companion.Attack();
        }
    }

    

    public void CheckNearbyEnemies()
    {
        _nearestDistance = 999;

        enemies.Clear();
        nearestEnemy = null;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, enemyDetectRadius);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Enemy"))
            {
                enemies.Add(hitColliders[i].GetComponent<Enemy>());

                _distance = Vector3.Distance(transform.position, hitColliders[i].transform.position);

                if (_distance < _nearestDistance)
                {
                    _nearestDistance = _distance;
                    nearestEnemy = hitColliders[i].GetComponent<Enemy>();
                    brain.agent.stoppingDistance = nearestEnemy.GetComponent<NavMeshAgent>().radius + enemyFollowStoppingDistance;
                }

                EnemyDetected?.Invoke(true);
            }
        }

        if(nearestEnemy == null)
        {
            EnemyDetected.Invoke(false);
        }
    }

    public override void OnStateExit()
    {
        AttackLoop(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
           CheckNearbyEnemies();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            CheckNearbyEnemies();
        }
    }
}
