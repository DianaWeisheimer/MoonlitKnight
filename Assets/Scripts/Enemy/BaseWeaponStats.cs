using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponStat", menuName = "WeaponStat")]
public class BaseWeaponStats : ScriptableObject
{
    public int damage;
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
}
