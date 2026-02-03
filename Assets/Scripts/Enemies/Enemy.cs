using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Character character;
    public AIBrain brain;

    [Header("State")]
    public bool dead;
    public bool hitable = true;

    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    public ParticleSystem damageParticle;
    public GameObject deathParticle;
    public AudioSource damageSource;

    public EnemyHealthBar healthBar;
    public Fae fae;
    public Vector2 onDeathXP;
    public Transform faePos;

    public Action onTakeHit;

    private void Awake()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
    }

    private void Start()
    {
        if(healthBar)healthBar.SetValue(character.stats.GetStat(StatType.Health));
    }

    private void Update()
    {
        character.aggroTable.DecayAggro(Time.deltaTime, 0.1f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            AttackStack stack = other.GetComponent<AttackStack>();
            if(stack == null) return;
            if(stack.attacker == null) return;
            if(stack.attacker.type == CharacterType.Player || stack.attacker.type == CharacterType.Companion)
            {
                TakeHit(other.GetComponent<AttackStack>(), other.ClosestPoint(transform.position));
            }
        }
    }

    public void TakeHit(AttackStack attackStack, Vector3 hitPosition)
    {
        if (dead) return;

        if (attackStack.bypassIFrames == false && !hitable) return;

        DamageStack stack = character.stats.TakeDamage(attackStack);
        DamagePopupGenerator.instance.CreatePopup(transform.position, stack.damageTaken);


        if (healthBar) healthBar.SetValue(character.stats.GetStat(StatType.Health));

        if (attackStack.hitParticle)
        {
            Instantiate(attackStack.hitParticle, hitPosition, Quaternion.identity);
        }

        brain.OnTakeHit(stack);

        if (stack.died) { Die(); return; }

        character.aggroTable.AddAggro(attackStack.attacker.type == CharacterType.Player ? attackStack.attacker : null, attackStack.TotalDamage());
        AggroManager.Instance.AddHostileEnemy(this);

        StartCoroutine(IFrames(0.1f));
        onTakeHit?.Invoke();
    }

    public void Die()
    {
        if (dead) return;
        dead = true;

        if (deathParticle)
            Instantiate(deathParticle, transform.position, Quaternion.identity);

        DropXP();

        DisableEnemy();
    }

    private void DropXP()
    {
        int xpToDrop = (int)UnityEngine.Random.Range(onDeathXP.x, onDeathXP.y);

        Vector3 dropPos = faePos ? faePos.position : transform.position;
        Fae newFae = Instantiate(fae, dropPos, Quaternion.identity);
        newFae.xpAmmount = xpToDrop;
    }

    private void DisableEnemy()
    {
        hitable = false;
        AggroManager.Instance.RemoveHostileEnemy(this);
        character.stats.GetStat(StatType.Health).currentValue = 0;

        // Disable AI
        brain.gameObject.SetActive(false);

        // Disable collider
        //GetComponent<Collider>().enabled = false;

        // Hide model
        if (character.characterModel)
            character.characterModel.gameObject.SetActive(false);
    }

    public void ResetEnemy()
    {
        dead = false;
        hitable = true;

        transform.position = spawnPosition;
        transform.rotation = spawnRotation;

        character.stats.SetCurrentValue();

        //GetComponent<Collider>().enabled = true;

        brain.gameObject.SetActive(true);
        brain.ResetBrain();
        character.characterModel.gameObject.SetActive(true);
    }

    public IEnumerator IFrames(float frames)
    {
        hitable = false;
        yield return new WaitForSeconds(frames);
        hitable = true;
    }
}
