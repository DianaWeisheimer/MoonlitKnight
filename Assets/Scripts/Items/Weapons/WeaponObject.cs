using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : ItemObject
{
    public MeshRenderer bladeRenderer;
    public BoxCollider boxCollider;
    public AttackStack attackStack;
    public bool overrideDamage;
    public Vector2 damageOverride;
    public AudioSource swingSource;
    public AudioSource hitSource;
    public Vector2 sourcePitchRange;
    public ParticleSystem particle;
    public ParticleSystem hitParticle;
    public ParticleSystem critHitParticle;

    [SerializeField] protected Character character;
    [SerializeField] protected Item item;
    [SerializeField] protected bool blocking;

    public override void OnInstantiate(Character character, InventorySlot item)
    {
        this.character = character;
        this.item = item.item;
        SetCollider(false);
    }

    public override void Use()
    {
        SetCollider(true);
    }

    public override void CancelUse()
    {
        SetCollider(false);
    }

    public virtual void SetCollider(bool hehe)
    {
        if (boxCollider)
        {
            boxCollider.enabled = hehe;

            if (particle && hehe) particle.Play();
            if (particle && !hehe) particle.Stop();

            if (hehe == true)
            {
                if (swingSource)
                {
                    float pitch = Random.Range(sourcePitchRange.x, sourcePitchRange.y);
                    swingSource.pitch = pitch;
                    swingSource.Play();
                }
            }
        }
    }

    public void SetBlocking(bool hehe)
    {
        blocking = hehe;
        attackStack.enabled = hehe;
    }

    public virtual void CalculateDamage(InventorySlot item, Character attacker, int damageBonus = 0, float damageMultiplier = 0)
    {
        if (item == null || item.item == null || attackStack == null)
        {
            return;
        }

        if (blocking)
        {
            attackStack.enabled = false;
            return;
        }

        attackStack.enabled = true;

        if (item.item is WeaponItem weaponItem)
        {
            bool crit = Random.Range(0, 100) < weaponItem.critChance;

            float multiplier = 1 + damageMultiplier / 100f; // Assuming damageMultiplier is a percentage
            float physicalDamage = Random.Range(weaponItem.physicalDamage.x, weaponItem.physicalDamage.y) * multiplier;
            float magicDamage = Random.Range(weaponItem.magicDamage.x, weaponItem.magicDamage.y) * multiplier;
            float fireDamage = Random.Range(weaponItem.fireDamage.x, weaponItem.fireDamage.y) * multiplier;
            float lightningDamage = Random.Range(weaponItem.lightningDamage.x, weaponItem.lightningDamage.y) * multiplier;
            float divineDamage = Random.Range(weaponItem.divineDamage.x, weaponItem.divineDamage.y) * multiplier;
            float occultDamage = Random.Range(weaponItem.occultDamage.x, weaponItem.occultDamage.y) * multiplier;
            float poiseDamage = weaponItem.poiseDamage;            

            // Apply scaling based on character stats
            physicalDamage = ApplyStatScaling(physicalDamage, weaponItem, attacker);
            magicDamage = ApplyStatScaling(magicDamage, weaponItem, attacker);
            fireDamage = ApplyStatScaling(fireDamage, weaponItem, attacker);
            lightningDamage = ApplyStatScaling(lightningDamage, weaponItem, attacker);
            divineDamage = ApplyStatScaling(divineDamage, weaponItem, attacker);
            occultDamage = ApplyStatScaling(occultDamage, weaponItem, attacker);

            float[] damages = { physicalDamage, magicDamage, fireDamage, lightningDamage, divineDamage, occultDamage, 0 };

            if (crit)
            {
                damages = ApplyCriticalDamage(damages);
                List<OnHitEffect> critEffects = GetCritEffects(weaponItem);
                attackStack.SetValues(attacker, DamageType.Physical, damages, false, poiseDamage, critHitParticle, critEffects);
            }

            else
            {
                attackStack.SetValues(attacker, DamageType.Physical, damages, false, poiseDamage, hitParticle, null);
            }

        }

        else
        {
            Debug.LogError("Item is not a WeaponItem.");
        }
    }

    private List<OnHitEffect> GetCritEffects(WeaponItem item)
    {
        List<OnHitEffect> effects = new List<OnHitEffect>();

        // Instantiate effects based on the OnHitEffectSO list in the WeaponItem
        foreach (var effectSO in item.critEffects)
        {
            OnHitEffect effect = effectSO.CreateOnHitEffect();
            effects.Add(effect);
        }

        return effects;
    }

    private float ApplyStatScaling(float stat, WeaponItem weaponItem, Character attacker)
    {
        float strengthBonus = stat * (attacker.stats.GetStat(StatType.Strength).maxValue / 100 * weaponItem.scaleStrength);
        float dexterityBonus = stat * (attacker.stats.GetStat(StatType.Dexterity).maxValue / 100 * weaponItem.scaleDexterity);
        float intelligenceBonus = stat * (attacker.stats.GetStat(StatType.Intelligence).maxValue / 100 * weaponItem.scaleIntelligence);
        float wisdomBonus = stat * (attacker.stats.GetStat(StatType.Wisdom).maxValue / 100 * weaponItem.scaleWisdom);

        stat = stat + strengthBonus + dexterityBonus + intelligenceBonus + wisdomBonus;

        return stat;
    }

    private float[] ApplyCriticalDamage(float[] damages)
    {
        for(int i = 0; i < damages.Length; i++)
        {
            damages[i] *= 2f; // Example: Increase damage by 50% on critical hit
        }

        return damages;
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("Enemy") && hitSource)
        {
            float pitch = Random.Range(sourcePitchRange.x, sourcePitchRange.y);
            hitSource.pitch = pitch;
            hitSource.Play();
        }*/
    }

}
