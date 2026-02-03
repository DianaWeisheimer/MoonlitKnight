using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateSearchPlayer : AIState
{
    public event Action PlayerNotFound;
    public float speed;
    public float angularSpeed;
    public float acceleration;
    public float wanderRange;
    public Vector3 startPosition;
    private Vector3 searchTarget;
    public Vector2 searchDelay;
    public Vector2 searchTime;

    public override void Tick()
    {
        if(brain.sensor.ScanForPlayer().Count > 0)
        {
            brain.PlayerFound();
        }
    }

    public override void OnStateEnter()
    {
        brain.agent.speed = speed;
        brain.agent.angularSpeed = angularSpeed;
        brain.agent.acceleration = acceleration;
        SetWanderTarget();
        StartCoroutine(SearchTimer());
    }

    public override void OnStateExit()
    {
        StopAllCoroutines();
    }

    public virtual void SetWanderTarget()
    {
        if (startPosition == Vector3.zero)
        {
            PlayerNotFound?.Invoke();
            return;
        }

        float posZ = UnityEngine.Random.Range(wanderRange, wanderRange * -1);
        float posX = UnityEngine.Random.Range(wanderRange, wanderRange * -1);

        searchTarget = new Vector3(startPosition.x += posX, startPosition.y, startPosition.z += posZ);

        brain.agent.SetDestination(searchTarget);

        StartCoroutine(NewAreaDelay());
    }

    private IEnumerator NewAreaDelay()
    {
        float timeToWait = UnityEngine.Random.Range(searchDelay.x, searchDelay.y);
        yield return new WaitForSeconds(timeToWait);
        SetWanderTarget();
    }

    private IEnumerator SearchTimer()
    {
        float timeToSearch = UnityEngine.Random.Range(searchTime.x, searchTime.y);
        yield return new WaitForSeconds(timeToSearch);
        PlayerNotFound?.Invoke();
    }
}
