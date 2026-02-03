using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Combat/Block")]
public class EnemyCombatBlock : AICombatSubState
{
    public Vector2 blockDuration = new Vector2(2, 10);
    public float blockRange = 5f;
    public Vector2 strafeDirectionDuration = new Vector2(2, 10);
    public float strafeSpeed = 2f;
    public float strafeAcceleration = 50f;

    private float currentStrafeDuration;
    private bool invertStrafe;
    private float strafeTime;
    private float exitTime;
    private float nextDecisionTime;

    public override void OnEnter()
    {
        exitTime = Time.time + UnityEngine.Random.Range(blockDuration.x, blockDuration.y);
        currentStrafeDuration = UnityEngine.Random.Range(strafeDirectionDuration.x, strafeDirectionDuration.y);
        strafeTime = Time.time + currentStrafeDuration;

        animator.SetBool("Blocking", true);
        brain.agent.speed = strafeSpeed;
        brain.agent.acceleration = strafeAcceleration;
        brain.agent.isStopped = false;
    }

    public override void Tick()
    {
        if (combatState.attackTarget == null)
        {
            return;
        }

        StrafeAroundTarget();

        if (combatState.CanBackstab(combatState.attackTarget) && Time.time > nextDecisionTime)
        {
            nextDecisionTime = Time.time + UnityEngine.Random.Range(0.1f, 0.3f);
            Emit(SubstateSignal.BackstabOpportunity);
        }

        if (Time.time >= exitTime)
        {
            Emit(SubstateSignal.BlockTimeout);
        }

        if (Time.time >= currentStrafeDuration + strafeTime)
        {
            strafeTime = Time.time;
            ChangeStrafeDirection();
        }

        float distance = Vector3.Distance(brain.transform.position, combatState.attackTarget.transform.position);

        if (distance < 2.2f && UnityEngine.Random.value < 0.01f)
        {
            Emit(SubstateSignal.CloseRangeAttack);
        }

        else if(distance > blockRange)
        {
            Emit(SubstateSignal.PlayerDisengage);
        }
    }

    public override void OnExit()
    {
        animator.SetBool("Blocking", false);
    }

    private void ChangeStrafeDirection()
    {
        currentStrafeDuration = UnityEngine.Random.Range(strafeDirectionDuration.x, strafeDirectionDuration.y);
        invertStrafe = !invertStrafe;
    }

    private void StrafeAroundTarget()
    {
        Vector3 dir = (brain.transform.position - combatState.attackTarget.transform.position).normalized;
        Vector3 strafe = Vector3.Cross(Vector3.up, dir);

        if(invertStrafe)
            strafe = -strafe;

        brain.agent.SetDestination(brain.transform.position + strafe * 5);
        combatState.lookAtPlayer = true;
    }
}


