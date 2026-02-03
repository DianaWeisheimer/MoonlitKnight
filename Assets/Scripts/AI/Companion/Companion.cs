using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Companion : MonoBehaviour, ICombatActor
{
    public CompanionMovement movement;
    //public AIBrain brain;
    public Character character; 
    public Animator animator;
    public NavMeshAgent agent;
    public bool dead;

    [Header("Attack")]
    private int comboIndex;
    private bool attackable;
    public float attackCooldown;
    private AnimationEvents animationEvents;

    private void Start()
    {
        character.hitable = true;
        attackable = true;
    }

    public void Initialize(Character _character, Animator _animator, AnimationEvents _animationEvents, NavMeshAgent _agent)
    {
        character = _character;
        animator = _animator;
        agent = _agent;
        animationEvents = _animationEvents;
        animationEvents.WeaponCollider += SetWeaponCollider;
    }

    public void Attack()
    {
        if (movement.grounded && attackable && !dead && !movement.dashing)
        {
            character.equipment.CalculateWeaponDamage(0);
            attackable = false;
            StartCoroutine(AttackCooldown());
            animator.SetTrigger("Attack" + comboIndex);

            comboIndex++;

            if (comboIndex > 3)
            {
                comboIndex = 0;
            }
        }
    }
    public void TakeHit(AttackStack attackStack, Vector3 hitPosition)
    {
        if (character.dead) return;

        if (attackStack.bypassIFrames == false && !character.hitable) return;

        DamageStack stack = character.stats.TakeDamage(attackStack);
        character.aggroTable.AddAggro(attackStack.attacker, stack.totalDamage);

        if (stack.died) { Die(); }
        //else if (stack.percentHealthLost > 20 + character.stats.GetStat(StatType.Poise).maxValue) { playerMovement.animator.SetTrigger("TakeHit"); }
        else if (stack.staggered) { animator.SetTrigger("TakeHit"); }

        if (attackStack.hitParticle)
        {
            Instantiate(attackStack.hitParticle, hitPosition, Quaternion.identity);
        }

        StartCoroutine(IFrames(0.1f));
    }

    public void Die()
    {       
        character.dead = true;  
    }

    public IEnumerator IFrames(float frames)
    {
        character.hitable = false;
        yield return new WaitForSeconds(frames);
        character.hitable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            AttackStack stack = other.GetComponent<AttackStack>();

            if (stack == null || stack.enabled == false) return;

            if (stack.attacker.type == CharacterType.Enemy)
            {
                TakeHit(other.GetComponent<AttackStack>(), other.ClosestPoint(transform.position));
            }
        }
    }

    public IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        attackable = true;
    }

    void ICombatActor.UseAbility(string v)
    {
        for(int i = 0; i < character.abilityHolder.ability.Count; i++)
        {
            if (character.abilityHolder.ability[i].abilitySO.name == v)
            {
                character.abilityHolder.abilityIndex = i;
                character.abilityHolder.AbilityPressed();
                break;
            }
        }
    }

    public void SetWeaponCollider(bool hehe, bool rightHand)
    {
        character.equipment.SetWeaponCollider(hehe, true);
    }

    public void OnDisable()
    {
        animationEvents.WeaponCollider -= SetWeaponCollider;
    }
}
