using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Companion/Role/Knight")]
public class CompanionRoleRogue : CompanionRoleProfile
{
    public float protectOffset = 0.4f;
    public float attackRange = 2.2f;
    public Vector2 attackDelay;

    private float nextAttackTime;
    private float attackTime;
    private float attackCombo;
    private Enemy target;

    public override void Initialize(CompanionBrain brain)
    {
        nextAttackTime = 0;
        brain.combat.lookAtTarget = true;
    }
    public override void ExecuteEngage(CompanionBrain brain)
    {
        target = GetTarget();
        if(target == null)
            return;

        brain.agent.stoppingDistance = 1.75f;
        brain.agent.SetDestination(target.transform.position);

        if (Time.time >= nextAttackTime && InRange(brain))
        {
            Attack(brain, false);
            ScheduleAttack();
        }   
    }

    public override Enemy GetTarget()
    {
        Enemy enemy = null;

        List<Enemy> hostileEnemies = AggroManager.Instance.GetAllHostileEnemies();
        Transform player = PartyManager.instance.GetActiveMember().core.transform;

        float distance = float.MaxValue;

        for (int i = 0; i < hostileEnemies.Count; i++)
        {
            float toEnemy = Vector3.Distance(hostileEnemies[i].transform.position, player.position);
            if(toEnemy < distance)
            {
                distance = toEnemy;
                enemy = hostileEnemies[i];
            }
        }

        return enemy;
    }

    public void Attack(CompanionBrain brain, bool heavyAttack)
    {
        if (heavyAttack)
        {
            //brain.character.stats.CheckStaminaCost(35);
            brain.character.equipment.CalculateWeaponDamage(50);
        }

        else
        {
            //brain.character.stats.CheckStaminaCost(25);
            brain.character.equipment.CalculateWeaponDamage(-75);
        }

        brain.character.animations.animator.SetTrigger("Attack" + attackCombo);
        brain.character.animations.animator.SetBool("HeavyAttack", heavyAttack);

        //GameEventsManager.instance.combatEvents.PlayerAttack();

        attackCombo++;
        attackTime = Time.time;

        if (attackCombo > 3)
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

    private bool InRange(CompanionBrain brain)
    {
        return Vector3.Distance(
            brain.transform.position,
            target.transform.position
        ) <= attackRange;
    }
}
