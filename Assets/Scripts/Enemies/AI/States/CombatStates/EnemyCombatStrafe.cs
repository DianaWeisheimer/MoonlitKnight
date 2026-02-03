using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Combat/Strafe")]
public class EnemyCombatStrafe : AICombatSubState
{
    public float strafeSpeed = 2.5f;
    public float strafeDurationMin = 1f;
    public float strafeDurationMax = 2f;

    private float endTime;
    private int direction; // -1 = left, 1 = right

    public override void OnEnter()
    {
        direction = Random.value > 0.5f ? 1 : -1;
        endTime = Time.time + Random.Range(strafeDurationMin, strafeDurationMax);

        brain.agent.isStopped = false;
        brain.agent.speed = strafeSpeed;
        brain.characterModel.animator.SetBool("Blocking", true);
    }

    public override void Tick()
    {
        if (brain.sensor.ScanForPlayer().Count < 0)
            return;

        Vector3 toTarget = combatState.attackTarget.transform.position - brain.transform.position;
        Vector3 strafeDir = Vector3.Cross(Vector3.up, toTarget).normalized * direction;

        Vector3 destination = brain.transform.position + strafeDir;
        brain.agent.SetDestination(destination);

        // Always face the player
        combatState.lookAtPlayer = true;

        if (Time.time >= endTime)
        {
            Emit(SubstateSignal.StrafeTimeout);
        }
    }

    public override void OnExit()
    {
        brain.characterModel.animator.SetBool("Blocking", false);
    }
}

