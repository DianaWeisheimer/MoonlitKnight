using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateStunned : AIState
{
    public event Action FinishedStun;
    public Vector2 stunTime;

    public override void Tick()
    {
        
    }

    public override void OnStateEnter()
    {
        StartCoroutine(Stun());
    }

    private IEnumerator Stun()
    {
        brain.agent.isStopped = true;

        float stunTimer = UnityEngine.Random.Range(stunTime.x, stunTime.y);
        yield return new WaitForSeconds(stunTimer);
        
        brain.agent.isStopped = false;
        Debug.Log("Finished Stun");

        FinishedStun?.Invoke();
    }

    public override void OnStateExit()
    {
        brain.agent.isStopped = false;
        StopAllCoroutines();
    }
}
