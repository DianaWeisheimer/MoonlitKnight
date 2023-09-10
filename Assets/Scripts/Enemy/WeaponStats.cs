using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public int baseDamage;
    public float damage;
    public int weight;

    public float healthScale;
    public float manaScale;
    public float staminaScale;
    public float vitalityScale;
    public float enduranceScale;
    public float wisdomScale;
    public float defenseScale;
    public float attackScale;
    public float dexterityScale;
    public float spiritScale;

    public float bonusHealth;
    public float bonusMana;
    public float bonusStamina;
    public float bonusVitality;
    public float bonusEndurance;
    public float bonusWisdom;
    public int bonusDefense;
    public int bonusAttack;
    public int bonusDexterity;
    public int bonusSpirit;

    public void LoadBaseStats(BaseWeaponStats baseStats, CharacterStats stats)
    {
        baseDamage = baseStats.damage;
        weight = baseStats.weight;


        damage = baseDamage;
        damage += stats.health * baseStats.healthScale;
        damage += stats.mana * baseStats.manaScale;
        damage += stats.stamina * baseStats.staminaScale;
        damage += stats.vitality * baseStats.vitalityScale;
        damage += stats.endurance * baseStats.enduranceScale;
        damage += stats.wisdom * baseStats.wisdomScale;
        damage += stats.defense * baseStats.defenseScale;
        damage += stats.attack * baseStats.attackScale;
        damage += stats.dexterity * baseStats.dexterityScale;
        damage += stats.spirit * baseStats.spiritScale;
    }
}
