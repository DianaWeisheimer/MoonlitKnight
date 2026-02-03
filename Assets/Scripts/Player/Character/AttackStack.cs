using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { Physical, Magic, Fire, Lightning, Divine, Occult, True }
public class AttackStack : MonoBehaviour
{
    public Character attacker;
    public DamageType type;
    public bool bypassIFrames;
    public float[] damages;
    public float poiseDamage;
    public ParticleSystem hitParticle;
    public List<OnHitEffect> critEffects;

    private void Awake()
    {
        if(!attacker) attacker = GetComponentInParent<Character>();
    }

    internal void SetValues(Character character, DamageType type, float[] damages, bool bypassIFrames = false,
        float poiseDamage = 0, ParticleSystem hitParticle = null, List<OnHitEffect> effects = null)
    {
        this.attacker = character;
        this.type = type;
        this.bypassIFrames = bypassIFrames;
        this.damages = damages;
        this.poiseDamage = poiseDamage;
        this.hitParticle = hitParticle;
        this.critEffects = effects;
    }

    public float TotalDamage()
    {
        float damage = 0;

        for(int i = 0; i < damages.Length; i++)
        {
            damage += damages[i];
        }

        return damage;
    }

    public void Blocked()
    {
        if(damages.Length == 0) { damages = new float[7]; }
        damages[0] = 0;
        poiseDamage = poiseDamage / 2;
    }
}
