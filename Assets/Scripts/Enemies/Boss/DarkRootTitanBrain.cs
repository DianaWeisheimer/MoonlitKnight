using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DarkRootTitanBrain : MonoBehaviour
{
    public DarkRootTitanBoss boss;
    [SerializeField] private int phase;
    public Transform aggroTarget;
    public Transform enemyMesh;
    public float faceTargetSpeed;
    public NavMeshAgent agent;
    private bool chasePlayer;
    public Vector2 attackCooldown;


    public void StartFight()
    {
        StartCoroutine(AttackCooldown());
        chasePlayer = true;
    }

    private void Update()
    {
        if (chasePlayer)
        {
            agent.SetDestination(aggroTarget.position);
        }

        FaceTarget();
    }

    private void AttackCheck()
    {
        Attack();
    }

    public void Stun()
    {

    }

    private void Attack()
    {
        int attackIndex = Random.Range(0, 2);
        //boss.animator.SetTrigger("Attack" + attackIndex);
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        float cooldown = Random.Range(attackCooldown.x, attackCooldown.y);
        yield return new WaitForSeconds(cooldown);
        AttackCheck();
    }

    private void FaceTarget()
    {
        Vector3 lookPos = aggroTarget.position - enemyMesh.position;
        lookPos.y = 0;

        Quaternion rotation = Quaternion.Slerp(enemyMesh.rotation, Quaternion.LookRotation(lookPos), faceTargetSpeed * Time.deltaTime);
        enemyMesh.rotation = rotation;
        //Quaternion rotation = Quaternion.LookRotation(lookPos);
        //enemyMesh.rotation = Quaternion.Slerp(transform.rotation, rotation, faceTargetSpeed);
    }
}
