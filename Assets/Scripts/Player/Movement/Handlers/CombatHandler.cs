using UnityEngine;
[System.Serializable]
public class CombatHandler
{
    private PlayerMovement movement;
    private float attackTime;
    public int comboIndex;

    public CombatHandler(PlayerMovement _movement)
    {
        movement = _movement;
    }

    public void Tick()
    {
        if (Time.deltaTime - attackTime > 2f)
        {
            comboIndex = 0;
        }
    }

    public void Attack(bool heavyAttack)
    {
        if (movement.combatMode)
        {
            //movement.attackable = false;

            if (heavyAttack)
            {
                movement.character.stats.CheckStaminaCost(35);
                movement.character.equipment.CalculateWeaponDamage(50);
            }

            else
            {
                movement.character.stats.CheckStaminaCost(25);
                movement.character.equipment.CalculateWeaponDamage(0);
            }

            movement.animator.SetTrigger("Attack" + comboIndex);
            movement.animator.SetBool("HeavyAttack", heavyAttack);

            GameEventsManager.instance.combatEvents.PlayerAttack();

            comboIndex++;
            attackTime = Time.time;

            if (comboIndex > 3)
            {
                comboIndex = 0;
            }
        }
    }

    public void Guard(bool hehe)
    {
        if (movement.combatMode)
        {
            if (hehe)
            {
                movement.animator.SetBool("Guarding", true);
            }

            else
            {
                movement.animator.SetBool("Guarding", false);
            }
        }
    }
}
