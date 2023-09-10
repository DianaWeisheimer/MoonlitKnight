using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public BaseCharacterStats baseStats;
    public CharacterStats stats;
    public bool hitable;
    public HealthBar healthBar;

    private void Awake()
    {
        stats.LoadBaseStats(baseStats);
        hitable = true;
        if (healthBar) { healthBar.UpdateHealthBar(stats); }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon") && hitable)
        {
            TakeDamage(other.GetComponent<Weapon>().damage);
        }
    }

    public void TakeDamage(float damage)
    {
        float damageTaken = damage - stats.defense;
        if(damageTaken < 0) { damageTaken = 0; }
        stats.health -= damageTaken;

        if (healthBar) { healthBar.UpdateHealthBar(stats); }
        if (healthBar) { healthBar.UpdateDamageText(damageTaken); }

        StartCoroutine(InvincibilityFrames());
    }

    public IEnumerator InvincibilityFrames()
    {
        hitable = false;
        yield return new WaitForSeconds(0.1f);
        hitable = true;
    }
}
