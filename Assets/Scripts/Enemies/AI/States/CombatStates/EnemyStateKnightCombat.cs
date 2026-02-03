using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStateKnightCombat : AIState
{
    [Header("Combat SubStates")]
    public AICombatSubState attackSubState;
    public AICombatSubState blockSubState;
    public AICombatSubState counterSubState;
    public AICombatSubState strafeSubState;

    private AICombatSubState currentSubState;
    public GameObject attackTarget;
    private bool chasePlayer;
    public bool lookAtPlayer;

    public override void OnStateEnter()
    {
        ChangeSubState(attackSubState);
    }

    public override void Tick()
    {
        currentSubState?.Tick();

        if (attackTarget == null)
        {
            GetTarget();
        }

        if (attackTarget && lookAtPlayer)
        {
            Vector3 direction = attackTarget.transform.position - brain.transform.position;
            Vector3 newDir = Vector3.RotateTowards(brain.agent.transform.forward, direction, 4 * Time.deltaTime, 0);
            newDir.y = 0;
            brain.agent.transform.rotation = Quaternion.LookRotation(newDir);
        }
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

    public override void OnStateExit()
    {
        currentSubState?.OnExit();
    }

    public void ChangeSubState(AICombatSubState newState)
    {
        currentSubState?.OnExit();
        currentSubState = Instantiate(newState); // IMPORTANT
        //currentSubState.Initialize(this);
        currentSubState.OnEnter();
    }
}


