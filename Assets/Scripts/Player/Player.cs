using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public PlayerCharacter character;

    public void Initialize(PlayerCharacter _character, PlayerMovement _movement)
    {
        character = _character;
        playerMovement = _movement;
    }

    private void Start()
    {
        character.hitable = true;
    }

    public void TakeHit(AttackStack attackStack, Vector3 hitPosition)
    {
        if (character.dead) return;

        if (attackStack.bypassIFrames == false && !character.hitable) return;

        DamageStack stack = character.stats.TakeDamage(attackStack);
        character.aggroTable.AddAggro(attackStack.attacker, stack.totalDamage);
 
        if (stack.died) { Die(); }
        //else if (stack.percentHealthLost > 20 + character.stats.GetStat(StatType.Poise).maxValue) { playerMovement.animator.SetTrigger("TakeHit"); }
        else if (stack.staggered) { playerMovement.animator.SetTrigger("TakeHit"); }

        if (attackStack.hitParticle)
        {
            Instantiate(attackStack.hitParticle, hitPosition, Quaternion.identity);
        }

        StartCoroutine(IFrames(0.1f));       
    }

    public void Die()
    {
        GameEventsManager.instance.combatEvents.PlayerDied();
        playerMovement.animator.SetTrigger("Die");
        playerMovement.FreezeMovement(true);
        character.dead = true;
        playerMovement.ChangeMovementType(1);
    }

    public IEnumerator IFrames(float frames)
    {
        character.hitable = false;
        yield return new WaitForSeconds(frames);
        character.hitable = true;
    }

    public void Respawn()
    {
        character.stats.SetCurrentValue();
        playerMovement.animator.SetTrigger("Respawn");
        character.dead = false;

        character.stats.CalculateDerivedStats();

        playerMovement.FreezeMovement(false);
        playerMovement.ChangeMovementType(0);
    }
    /*public IEnumerator Respawn(Vector3 position)
    {
        character.stats.SetCurrentValue();
        playerMovement.animator.SetTrigger("Respawn");
        character.dead = false;
        playerMovement.controller.transform.position = position;
        character.stats.CalculateDerivedStats();
        yield return new WaitForFixedUpdate();
        playerMovement.FreezeMovement(false);
        playerMovement.ChangeMovementType(0);
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(6);

        if (character.dead)
        {
            StartCoroutine(Respawn(Vector3.zero));
        }
    }*/


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
}
