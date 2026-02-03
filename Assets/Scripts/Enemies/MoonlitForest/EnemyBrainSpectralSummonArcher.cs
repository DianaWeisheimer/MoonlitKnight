using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrainSpectralSummonArcher : AIBrain
{
    public AnimationEvents animationEvent;
    public Vector2 losePlayerTime;
    public override void PlayerFound()
    {
        //ChangeState(states[1]);
    }

    public override void PlayerLost()
    {
        //StartCoroutine(LosePlayer());
    }

    public IEnumerator LosePlayer()
    {
        float timeToWait = Random.Range(losePlayerTime.x, losePlayerTime.y);
        yield return new WaitForSeconds(timeToWait);

        //if (!playerInSight)
        //{
            //ChangeState(states[0]);
        //}
    }

    public override void OnPlayerDied()
    {
        //ChangeState(states[0]);
    }

    private void AttackStart(bool hehe)
    {
        if (hehe)
        {
            agent.isStopped = false;
        }

        else 
        {
            agent.isStopped = true;
        }
    }


    private void OnEnable()
    {
        animationEvent.Attack += AttackStart;
    }
}
