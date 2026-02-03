using UnityEngine;

[CreateAssetMenu(menuName = "AI/Combat/Backstab")]
public class EnemyCombatBackstab : AICombatSubState
{
    [Header("Ranges")]
    public float engageRange = 5f;    // distance at which this state can be entered
    public float attackRange = 2f;    // actual strike range

    [Header("Movement")]
    public float approachSpeed = 6f;
    public float approachAcceleration = 25f;

    [Header("Timing")]
    public float maxChaseTime = 1.5f; // failsafe to prevent infinite chase

    private float chaseEndTime;
    private float exitTime;
    private bool attackStarted;

    public override void OnEnter()
    {
        if (combatState.attackTarget == null)
        {
            Emit(SubstateSignal.AttackTimeout);
            return;
        }

        chaseEndTime = Time.time + maxChaseTime;
        attackStarted = false;

        brain.agent.speed = approachSpeed;
        brain.agent.acceleration = approachAcceleration;
        brain.agent.isStopped = false;

        combatState.lookAtPlayer = true;

        animator.SetBool("Sprinting", true);
    }

    public override void Tick()
    {
        if (combatState.attackTarget == null)
        {
            Emit(SubstateSignal.AttackTimeout);
            return;
        }

        float distance = Vector3.Distance(brain.transform.position, combatState.attackTarget.transform.position);

        // Move towards target
        if(!attackStarted) brain.agent.SetDestination(combatState.attackTarget.transform.position);

        // If close enough ? attack once
        if (!attackStarted && distance <= attackRange)
        {
            brain.agent.acceleration = 150f;
            PerformBackstab();
        }

        // Safety exit
        if (Time.time >= chaseEndTime)
        {
            Emit(SubstateSignal.AttackTimeout);
        }

        if (attackStarted && Time.time >= exitTime)
        {
            Emit(SubstateSignal.AttackTimeout);
        }
    }

    public override void OnExit()
    {
        animator.SetBool("Sprinting", false);
        brain.agent.isStopped = false;
    }

    private void PerformBackstab()
    {
        animator.SetBool("Sprinting", false);
        combatState.lookAtPlayer = false;

        attackStarted = true;

        brain.agent.speed = 5;
        brain.agent.acceleration = 25;
        brain.agent.isStopped = true;
        brain.agent.ResetPath();

        animator.SetTrigger("Attack0");

        exitTime = Time.time + 1f;
    }
}
