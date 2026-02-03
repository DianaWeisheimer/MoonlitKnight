using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateWander : AIState
{
    public float speed;
    public float angularSpeed;
    public float acceleration;
    public float wanderRange;
    public Vector3 startPosition;
    private Vector3 wanderTarget;
    public Vector2 wanderDelay;

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

    private void Start()
    {
        startPosition = transform.position;
    }

    public override void OnStateEnter()
    {
        brain.agent.speed = speed;
        brain.agent.angularSpeed = angularSpeed;
        brain.agent.acceleration = acceleration;
        SetWanderTarget();
    }

    public override void OnStateExit()
    {
        StopAllCoroutines();
    }

    public virtual void SetWanderTarget()
    {
        float posZ = Random.Range(wanderRange, wanderRange * -1);
        float posX = Random.Range(wanderRange, wanderRange * -1);

        wanderTarget = new Vector3(startPosition.x += posX, startPosition.y, startPosition.z += posZ);

        brain.agent.SetDestination(wanderTarget);

        StartCoroutine(NewAreaDelay());
    }

    private IEnumerator NewAreaDelay()
    {
        float timeToWait = Random.Range(wanderDelay.x, wanderDelay.y);
        yield return new WaitForSeconds(timeToWait);
        SetWanderTarget();
    }
}
