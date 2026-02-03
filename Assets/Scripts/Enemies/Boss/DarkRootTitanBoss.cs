using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRootTitanBoss : MonoBehaviour
{
    /*public DarkRootTitanBrain brain;
    public Character character;
    public ParticleSystem damageParticle;
    public AudioSource damageSource;
    private Vector3 hitPosition;
    public Animator animator;

    [Header("Stun")]
    public int stunTreshold;
    public int stunDamageTaken;
    public float stunResetTime;
    public float lastHitTime;
    public float stunCooldown;
    public bool stunable;

    private void Update()
    {
        animator.SetFloat("Speed", brain.agent.velocity.magnitude);
        if (Time.time - lastHitTime > stunResetTime)
        {
            stunDamageTaken = 0;
        }
    }

    public override void StartBossFight()
    {
        stunable = true;
        hitable = true;
        character.stats.InitializeStats();
        brain.StartFight();
        healthBar.gameObject.SetActive(true);
        healthBar.SetValue(character.stats.GetStat(StatType.Health));
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            hitPosition = other.ClosestPoint(transform.position);

            AttackStack stack = other.GetComponent<AttackStack>();

            if (stack.attackOrigin == CharacterType.Player || stack.attackOrigin == CharacterType.Companion)
            {
                TakeHit(other.GetComponent<AttackStack>());
            }
        }
    }

    public void TakeHit(AttackStack attackStack)
    {
        if (dead) return;

        if (attackStack.bypassIFrames == false && hitable)
        {
            DamageStack stack = character.stats.TakeDamage((int)attackStack.damage);
            healthBar.SetValue(character.stats.GetStat(StatType.Health));

            lastHitTime = Time.time;
            stunDamageTaken += (int)attackStack.damage;

            if(stunDamageTaken >= stunTreshold && stunable)
            {
                brain.Stun();
                StartCoroutine(StunCooldown());
            }

            //if (stack.died) { Die(); }

            damageParticle.transform.position = hitPosition;
            damageParticle.Stop();
            damageParticle.Play();
            damageSource.Play();
            //else if (stack.percentHealthLost > 20) { playerMovement.animator.SetTrigger("TakeHit"); }

           // StartCoroutine(IFrames());
        }

        else if (attackStack.bypassIFrames == true)
        {
            DamageStack stack = character.stats.TakeDamage((int)attackStack.damage);
            healthBar.SetValue(character.stats.GetStat(StatType.Health));

            lastHitTime = Time.time;
            stunDamageTaken += (int)attackStack.damage;

            if (stunDamageTaken >= stunTreshold && stunable)
            {
                brain.Stun();
                StartCoroutine(StunCooldown());
            }

            //if (stack.died) { Die(); }

            damageParticle.transform.position = hitPosition;
            damageParticle.Stop();
            damageParticle.Play();
            damageSource.Play();
            //else if (stack.percentHealthLost > 20) { playerMovement.animator.SetTrigger("TakeHit"); }

            //StartCoroutine(IFrames());
        }
    }

    private IEnumerator StunCooldown()
    {
        stunable = false;
        yield return new WaitForSeconds(stunCooldown);
        stunDamageTaken = 0;
        stunable = true;
    }*/
}
