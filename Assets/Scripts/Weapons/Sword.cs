using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public ParticleSystem trail;
    public BoxCollider attackCollider;

    public override void OnAttack()
    {
        trail.Stop();
        trail.Play();
    }

    public override void StartCollider()
    {
        damage = stats.damage;
        float randomMultiplier = Random.Range(0.9f, 1.1f);
        damage = damage * randomMultiplier;
        attackCollider.enabled = true;
    }

    public override void StopCollider()
    {
        attackCollider.enabled = false;
    }
}
