using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrainMoonlightStalker : AIBrain
{
    public Vector2 losePlayerTime;
    public EnemyTerritory territory;
    public GameObject prey;

    private void Start()
    {
        if (!territory)
        {
            //ChangeState(states[3]);
        }
    }

    public override void PlayerFound()
    {
        //ChangeState(states[1]);
    }

    public override void PlayerLost()
    {
        StartCoroutine(LosePlayer());
    }

    public void EnteredTerritory()
    {
        if (CheckClosestTargetInTerritory()) 
        {
            //ChangeState(states[1]);
        }
    }

    public void LeftTerritory()
    {
        if (CheckClosestTargetInTerritory())
        {
            //ChangeState(states[1]);
        }

        else
        {
            //ChangeState(states[0]);
        }
    }

    public bool CheckClosestTargetInTerritory()
    {
        if(territory.intruders != null)
        {
            float nearestDistance = 999;
            float distance;
        
            for (int i = 0; i < territory.intruders.Count; i++)
            {
                distance = Vector3.Distance(transform.position, territory.intruders[i].transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    prey = territory.intruders[i];
                }           
            }

            return true;
        }

        else
        {
            return false;
        }
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

    private void OnEnable()
    {
        if (territory)
        {
            territory.EnteredTerritorry += EnteredTerritory;
        }
    }

    private void OnDisable()
    {
        if (territory)
        {
            territory.EnteredTerritorry -= EnteredTerritory;
        }
    }
}
