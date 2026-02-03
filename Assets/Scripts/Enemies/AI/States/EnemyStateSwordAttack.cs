using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EnemyStateSwordAttack : AIState
{
    public GameObject attackTarget;
    public Vector3 targetLastPosition;
    public Vector2 startAttackDelay;
    public Vector2 chargeDelay;
    public float meleeAttackRange;
    public bool chasePlayer;
    public bool lookAtPlayer;
    
    [Header("Movement Stats")]
    public float baseSpeed;
    public float baseAngularSpeed;
    public float baseAcceleration;
    
    [Header("Charge")]
    public float chargeSpeed;
    public float chargeAngularSpeed;
    public float chargeAcceleration;

    [Header("Player Search")]
    public float losePlayerTime;
    public float lostPlayerTime;
    public override void Tick()
    {
        if (attackTarget == null)
        {
            GetTarget();
        }

        if (attackTarget && lookAtPlayer)
        {
            Vector3 direction = attackTarget.transform.position - brain.transform.position;
            Vector3 newDir = Vector3.RotateTowards(brain.agent.transform.forward, direction, 6 * Time.deltaTime, 0);
            brain.agent.transform.rotation = Quaternion.LookRotation(newDir);
        }

        if (chasePlayer)
        {
            brain.agent.SetDestination(attackTarget.transform.position);
            targetLastPosition = attackTarget.transform.position;
        }

        if (brain.sensor.ScanForPlayer().Count > 0)
        {
            lostPlayerTime = Time.time;
        }

        if (Time.time - lostPlayerTime > losePlayerTime)
        {
            brain.PlayerLost();
        }
    }

    public override void OnStateEnter()
    {
        GetTarget();

        if(attackTarget == null)
        {
            brain.PlayerNotFound();
        }
        
        StartCoroutine(StartAttack());

        brain.agent.speed = baseSpeed;
        brain.agent.angularSpeed = baseAngularSpeed;
        brain.agent.acceleration = baseAcceleration;

        lookAtPlayer = true;
    }

    public override void OnStateExit()
    {
        StopAllCoroutines();
        brain.characterModel.animator.SetBool("Charging", false);
    }

    private void GetTarget()
    {
        // Scan visible targets from the sensor
        List<GameObject> visibleTargets = brain.sensor.ScanForPlayer();
        if (visibleTargets == null || visibleTargets.Count == 0)
        {
            return;
        }

        // Ask the AggroTable for the target with the most aggro
        Character highestAggroTarget = brain.character.aggroTable.GetHighestAggroTarget(
            (ch) => visibleTargets.Contains(ch.gameObject) // filter: must be visible
        );

        if (highestAggroTarget != null)
        {
            attackTarget = highestAggroTarget.gameObject;
            chasePlayer = true;
        }
        else
        {
            // fallback: prefer the Player if visible
            GameObject player = visibleTargets.FirstOrDefault(t => t.CompareTag("Player"));
            attackTarget = player != null ? player : visibleTargets[0];
            chasePlayer = attackTarget != null;
        }
    }

    private IEnumerator StartAttack()
    {
        GetTarget();
        if (attackTarget == null) yield return null;
        else
        {
            float timeToWait = Random.Range(startAttackDelay.x, startAttackDelay.y);
            yield return new WaitForSeconds(timeToWait);

            float distance = Vector3.Distance(transform.position,attackTarget.transform.position);

            if(distance < meleeAttackRange)
            {
                MeleeAttack();
            }

            else
            {
                StartCoroutine(ChargeAttack());
            }
        }
    }

    private void MeleeAttack()
    {
        brain.characterModel.animator.SetTrigger("Attack");
        StartCoroutine(StartAttack());
    }

    private IEnumerator ChargeAttack()
    {
        chasePlayer = false;
        brain.agent.SetDestination(attackTarget.transform.position);
        brain.agent.speed = 1;
        brain.agent.angularSpeed = 1000;

        float timeToWait = Random.Range(chargeDelay.x, chargeDelay.y);
        yield return new WaitForSeconds(timeToWait);

        brain.characterModel.animator.SetBool("Charging", true);
        brain.agent.speed = chargeSpeed;
        brain.agent.angularSpeed = chargeAngularSpeed;
        brain.agent.acceleration = chargeAcceleration;
        lookAtPlayer = false;

        yield return new WaitForSeconds(3);

        brain.characterModel.animator.SetBool("Charging", false);
        chasePlayer = true;
        lookAtPlayer = true;
        brain.agent.speed = baseSpeed;
        brain.agent.angularSpeed = baseAngularSpeed;
        brain.agent.acceleration = baseAcceleration;
        StartCoroutine(StartAttack());
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.cyan;
        Handles.Label(transform.position + Vector3.up * 3,
            $"Lose Player: {Time.time - lostPlayerTime}", style);
    }
#endif
}
