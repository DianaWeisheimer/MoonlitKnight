using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Combat/Attack")]
public class EnemyCombatAttack : AICombatSubState
{
    public Vector2 attackDuration = new Vector2(2, 10);
    public float meleeAttackRange;
    public Vector2 attackDelay;
    public bool canHeavyAttack;
    public float attackMoveSpeed = 6;
    public float attackAcceleration = 50;
    public float disengageRange = 15;

    private float nextAttackTime;
    private float exitTime;
    private int attackCombo;

    public override void OnEnter()
    {
        exitTime = Time.time + Random.Range(attackDuration.x, attackDuration.y);
        ScheduleAttack();
        brain.agent.speed = attackMoveSpeed;
        brain.agent.acceleration = attackAcceleration;
    }

    public override void Tick()
    {
        if (combatState.attackTarget == null)
        {
            return;
        }

        MoveAndRotate();

        if (Time.time >= nextAttackTime && InRange())
        {
            Attack();
            ScheduleAttack();
        }

        if (Time.time >= exitTime)
        {
            Emit(SubstateSignal.AttackTimeout);
        }

        float distance = Vector3.Distance(brain.transform.position, combatState.attackTarget.transform.position);

        if (distance > disengageRange)
        {
            Emit(SubstateSignal.PlayerDisengage);
        }
    }

    public override void OnExit() { }

    private void MoveAndRotate()
    {
        brain.agent.SetDestination(combatState.attackTarget.transform.position);
    }

    private bool InRange()
    {
        return Vector3.Distance(
            brain.transform.position,
            combatState.attackTarget.transform.position
        ) <= meleeAttackRange;
    }

    public void Attack()
    {
        bool heavyAttack = canHeavyAttack && Random.Range(0, 100) < 25;

        if (heavyAttack)
        {
            brain.character.equipment.CalculateWeaponDamage(50);
        }
        else
        {
            brain.character.equipment.CalculateWeaponDamage(0);
        }

        brain.character.animations.animator.SetBool("HeavyAttack", heavyAttack);
        brain.character.animations.animator.SetTrigger("Attack" + attackCombo);

        //GameEventsManager.instance.combatEvents.PlayerAttack();

        attackCombo++;

        if (attackCombo > 1)
        {
            attackCombo = 0;
        }
    }

    private void ScheduleAttack()
    {
        nextAttackTime = Time.time + Random.Range(
            attackDelay.x,
            attackDelay.y
        );
    }
}


