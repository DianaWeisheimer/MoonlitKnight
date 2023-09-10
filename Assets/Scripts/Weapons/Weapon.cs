using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public CharacterStats wielderStats;
    public BaseWeaponStats baseStats;
    public WeaponStats stats;
    public float damage;

    private void Start()
    {
        LoadStats();
    }

    public void LoadStats()
    {
        stats.LoadBaseStats(baseStats, wielderStats);
        damage = stats.damage;
    }

    public virtual void OnAttack()
    {

    }

    public virtual void StartCollider()
    {

    }

    public virtual void StopCollider()
    {

    }

    public virtual void Shoot()
    {

    }
}
