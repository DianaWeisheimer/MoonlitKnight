using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateSpectralSummonWarriorAttack : AIState
{
    public Vector2 startAttackDelay;
    public Vector2 meleeAttackStartDelay;
    public Vector2 meleeAttackFinishDelay;
    public float meleeAttackRange;
    public bool chasePlayer;
    public float chargeAngularSpeed;
    public float chargeSpeed;
    public float chargeAcceleration;
    public Transform enemyMesh;
    public float faceTargetSpeed;
    public AttackStack attackStack;
    public Vector2 damageRange;
    public override void Tick()
    {
        if (chasePlayer)
        {
            //brain.agent.SetDestination(brain.player.transform.position);
            FaceTarget();
        }
    }

    public override void OnStateEnter()
    {
        chasePlayer = true;

        StartAttack();
    }

    public override void OnStateExit()
    {
        StopAllCoroutines();
    }

    private void FaceTarget()
    {
        //Vector3 lookPos = brain.player.transform.position - enemyMesh.position;
        //lookPos.y = 0;

        //Quaternion rotation = Quaternion.Slerp(enemyMesh.rotation, Quaternion.LookRotation(lookPos), faceTargetSpeed * Time.deltaTime);
        //enemyMesh.rotation = rotation;
        //Quaternion rotation = Quaternion.LookRotation(lookPos);
        //enemyMesh.rotation = Quaternion.Slerp(transform.rotation, rotation, faceTargetSpeed);
    }

    private void CalculateDamage()
    {
        float damage = Random.Range(damageRange.x, damageRange.y);

        attackStack.attacker.type = CharacterType.Enemy;
        //attackStack.damage = damage;
    }

    private void StartAttack()
    {
        StopAllCoroutines();
        //StartCoroutine(CheckAttackRange());
    }

    //private IEnumerator CheckAttackRange()
    //{
        //float distance = Vector3.Distance(transform.position, brain.player.transform.position);

        //if (distance < meleeAttackRange)
        //{
        //    StartCoroutine(MeleeAttack());
        //}

        //else
        //{
        //    yield return new WaitForSeconds(startAttackDelay.x);
        //    StartAttack();
        //}
    //}

    private IEnumerator MeleeAttack()
    {
        float baseSpeed = brain.agent.speed;
        float baseAccel = brain.agent.acceleration;
        float baseAngularSpeed = brain.agent.angularSpeed;
        brain.agent.speed = 1;
        brain.agent.angularSpeed = 1000;

        float startDelay = Random.Range(meleeAttackStartDelay.x, meleeAttackStartDelay.y);
        yield return new WaitForSeconds(startDelay);

        chasePlayer = false;
        int attackIndex = Random.Range(0, 3);
        CalculateDamage();
        brain.characterModel.animator.SetTrigger("Attack" + attackIndex);

        yield return new WaitForSeconds(1.5f);

        chasePlayer = true;
        brain.agent.speed = baseSpeed;
        brain.agent.angularSpeed = baseAngularSpeed;
        brain.agent.acceleration = baseAccel;

        float finishDelay = Random.Range(meleeAttackFinishDelay.x, meleeAttackFinishDelay.y);
        yield return new WaitForSeconds(finishDelay);
        StartAttack();
    }
}
