using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrainForestMangler : MonoBehaviour
{
    public NavMeshAgent agent;
    private GameObject player;
    public Animator animator;
    public bool chasing;
    public Transform[] scoutTarget;
    public int currentTarget;
    public EnemyAIAttack[] attacks;
    private bool attackable;

    private void Start()
    {
        attackable = true;
        SetScoutTarget();
    }

    void Update()
    {
        Tick();
    }

    public virtual void Tick()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (chasing)
        {
            agent.SetDestination(player.transform.position);
            if(attackable) Attack();
        }
    }

    public virtual void SetScoutTarget()
    {
        if (!chasing)
        {
            agent.SetDestination(scoutTarget[currentTarget].position);
        }
    }

    public virtual void ChasePlayer()
    {
        chasing = true;
    }

    public virtual void StopChasingPlayer()
    {
        chasing = false;
        SetScoutTarget();
    }

    public virtual void FreezeMovement(bool freeze)
    {
        //agent.isStopped = freeze;
    }

    public virtual void Attack()
    {
        for(int i = 0; i < attacks.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if(distance <= attacks[i].attackTriggerRange)
            {
                //animator.runtimeAnimatorController = attacks[i].attackAnimation;
                //animator.SetTrigger("Attack");
                StartCoroutine(AttackCooldown(attacks[i]));
                return;
            }
        }
    }

    private IEnumerator AttackCooldown(EnemyAIAttack aIAttack)
    {
        attackable = false;
        yield return new WaitForSeconds(aIAttack.attackCooldown);
        attackable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            ChasePlayer();
        }

        else if (other.CompareTag("ScoutTarget"))
        {
            currentTarget++;
            if(currentTarget >= scoutTarget.Length) { currentTarget = 0; }
            SetScoutTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopChasingPlayer();
        }
    }
}
