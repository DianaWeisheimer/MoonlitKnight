using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public BaseCharacterStats baseStats;
    public CharacterStats stats;
    public bool hitable;
    public PlayerHealthBar healthBar;
    public PlayerInventoryController inventoryController;
    public PlayerMovement movement;

    private void Awake()
    {
        if (!healthBar) { healthBar = FindObjectOfType<PlayerHealthBar>(); }
        stats.LoadBaseStats(baseStats);
        hitable = true;
        if (healthBar) { healthBar.UpdateHealthBar(stats); }
    }

    public void FixedUpdate()
    {
        PassiveHealing();
    }

    public void PassiveHealing()
    {
        stats.health += stats.vitality;
        stats.stamina += stats.endurance;
        stats.mana += stats.wisdom;

        if(stats.health >= stats.maxHealth) { stats.health = stats.maxHealth; }
        if(stats.stamina >= stats.maxStamina) { stats.stamina = stats.maxStamina; }
        if(stats.mana >= stats.maxMana) { stats.mana = stats.maxMana; }

        healthBar.UpdateHealthBar(stats);
    }

    public void DrainMana(int ammount)
    {
        stats.mana += ammount;
    }

    public void DrainStamina(int ammount)
    {
        stats.stamina += ammount;
    }

    public void Sprint(bool sprinting)
    {
        if (sprinting) { stats.endurance = stats.endurance / 2; }
        else if (!sprinting) { stats.endurance = stats.endurance * 2; }
    }

    public void Meditate(bool meditating)
    {
        if (meditating)
        {
            movement.meditating = true;
            stats.vitality = stats.vitality * (2 + (stats.spirit * 0.1f));
            stats.endurance = stats.endurance * (2 + (stats.spirit * 0.1f));
            stats.wisdom = stats.wisdom * (2 + (stats.spirit * 0.1f));
        }

        else if (!meditating)
        {
            movement.meditating = false;
            stats.vitality = stats.vitality / (2 + (stats.spirit * 0.1f));
            stats.endurance = stats.endurance / (2 + (stats.spirit * 0.1f));
            stats.wisdom = stats.wisdom / (2 + (stats.spirit * 0.1f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon"))
        {
            TakeDamage(other.GetComponent<Weapon>().damage);
        }
    }

    public void TakeDamage(float damage)
    {
        float damageTaken = damage - stats.defense;
        if (damageTaken < 0) { damageTaken = 0; }
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
