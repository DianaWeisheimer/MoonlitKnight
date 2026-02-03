using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyStatePatrol : AIState
{
    public event Action NoScoutArea;
    public float speed;
    public float angularSpeed;
    public float acceleration;
    public List<Transform> patrolPoints;
    public int currentTarget;
    public Vector2 changeAreaDelay;

    public override void Tick()
    {
        if (brain.sensor.ScanForPlayer().Count > 0)
        {
            brain.PlayerFound();
        }

        else
        {
            return;
        }
    }

    public override void OnStateEnter()
    {
        brain.agent.speed = speed;
        brain.agent.angularSpeed = angularSpeed;
        brain.agent.acceleration = acceleration;

        if (patrolPoints.Count == 0) 
        {
            NoScoutArea?.Invoke();
            return;
        }

        else if(patrolPoints[0] == null)
        {
            NoScoutArea?.Invoke();
            return;
        }

        else
        {
            SetScoutTarget();
        }
    }

    public override void OnStateExit()
    {
        StopAllCoroutines();
    }

    public virtual void SetScoutTarget()
    {
        if(patrolPoints == null || patrolPoints.Count == 0) { return; }
        if (currentTarget >= patrolPoints.Count) { currentTarget = 0; }
        brain.agent.SetDestination(patrolPoints[currentTarget].position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScoutTarget"))
        {
            currentTarget++;
            if (currentTarget >= patrolPoints.Count) { currentTarget = 0; }
            StartCoroutine(NewAreaDelay());
        }
    }

    private IEnumerator NewAreaDelay()
    {
        float timeToWait = UnityEngine.Random.Range(changeAreaDelay.x, changeAreaDelay.y);
        yield return new WaitForSeconds(timeToWait);
        SetScoutTarget();
    }
}
