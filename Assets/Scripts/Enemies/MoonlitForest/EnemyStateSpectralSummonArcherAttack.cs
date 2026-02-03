using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateSpectralSummonArcherAttack : AIState
{
    /*public AnimationEvents animationEvents;
    public BowObject bowObject;
    public Vector2 meleeAttackDelay;
    public Vector2 rangedAttackDelay;
    public Vector2 attackDelay;
    public float meleeAttackRange;
    public float rangedttackRange;
    public bool chasePlayer;
    public Transform enemyMesh;
    public float faceTargetSpeed;
    public Vector2 meleeDamageRange;
    public Transform aimTarget;
    public override void Tick()
    {
        aimTarget.transform.position = new Vector3(brain.player.transform.position.x, brain.player.transform.position.y + 1.5f, brain.player.transform.position.z);

        if (chasePlayer)
        {
            brain.agent.SetDestination(brain.player.transform.position);
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
        Vector3 lookPos = brain.player.transform.position - enemyMesh.position;
        lookPos.y = 0;

        Quaternion rotation = Quaternion.Slerp(enemyMesh.rotation, Quaternion.LookRotation(lookPos), faceTargetSpeed * Time.deltaTime);
        enemyMesh.rotation = rotation;
        //Quaternion rotation = Quaternion.LookRotation(lookPos);
        //enemyMesh.rotation = Quaternion.Slerp(transform.rotation, rotation, faceTargetSpeed);
    }

    /*private AttackStack CalculateMeleeDamage()
    {
        AttackStack attackStack = new AttackStack();

        float damage = Random.Range(meleeDamageRange.x, meleeDamageRange.y);

        attackStack.character.type = CharacterType.Enemy;
        attackStack.damage = damage;

        return attackStack;
    }

    private void StartAttack()
    {
        StopAllCoroutines();
        StartCoroutine(CheckAttackRange());
    }

    private IEnumerator CheckAttackRange()
    {
        float timeToWait = Random.Range(attackDelay.x, attackDelay.y);
        yield return new WaitForSeconds(timeToWait);

        float distance = Vector3.Distance(transform.position, brain.player.transform.position);


        if(distance > rangedttackRange)
        {
            chasePlayer = true;
            StartAttack();
        }

        else if(distance <= rangedttackRange)
        {
            StartCoroutine(RangedAttack());
        }

        if (distance < meleeAttackRange)
        {
            Debug.Log("Melee");
            StartCoroutine(MeleeAttack());
        }

        else if(distance >= meleeAttackRange && distance < rangedttackRange)
        {
            Debug.Log("Ranged");
            StartCoroutine(RangedAttack());
        }

        else
        {          
            StartAttack();
            chasePlayer = true;
        }
    }

    private IEnumerator MeleeAttack()
    {
        float baseSpeed = brain.agent.speed;
        float baseAccel = brain.agent.acceleration;
        float baseAngularSpeed = brain.agent.angularSpeed;
        brain.agent.speed = 1;
        brain.agent.angularSpeed = 1000;

        chasePlayer = false;
        int attackIndex = Random.Range(0, 2);
        //CalculateMeleeDamage();
        brain.animator.SetTrigger("Attack" + attackIndex);

        yield return new WaitForSeconds(1.5f);

        //chasePlayer = true;
        brain.agent.speed = baseSpeed;
        brain.agent.angularSpeed = baseAngularSpeed;
        brain.agent.acceleration = baseAccel;

        float timeToWait = Random.Range(meleeAttackDelay.x, meleeAttackDelay.y);
        yield return new WaitForSeconds(timeToWait);

        StartAttack();
    }
    private IEnumerator RangedAttack()
    {
        brain.agent.speed = 1;
        chasePlayer = false;
        brain.animator.SetTrigger("AttackBow");

        yield return new WaitForSeconds(1.5f);

        chasePlayer = true;

        float timeToWait = Random.Range(rangedAttackDelay.x, rangedAttackDelay.y);
        yield return new WaitForSeconds(timeToWait);

        StartAttack();
    }

    private void Shoot(bool hehe)
    {
        if (hehe)
        {
            bowObject.Use();
        }
    }

    private void OnEnable()
    {
        animationEvents.WeaponCollider += Shoot;
    }*/
}
