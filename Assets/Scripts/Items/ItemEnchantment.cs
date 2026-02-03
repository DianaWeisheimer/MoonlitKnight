using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnchantment", menuName = "Item/New Enchantment")]
public class ItemEnchantment : ScriptableObject
{
    public int enchantID;
    
    public float healthBonus;
    public float healthMultiplier;

    public float manaBonus;
    public float manaMultiplier;

    public float staminaBonus;
    public float staminaMultiplier;

    public float vitalityBonus;
    public float vitalityMultiplier;

    public float enduranceBonus;
    public float enduranceMultiplier;

    public float arcaneBonus;
    public float arcaneMultiplier;

    public float strengthBonus;
    public float strengthMultiplier;

    public float dexterityBonus;
    public float dexterityMultiplier;

    public float intelligenceBonus;
    public float intelligenceMultiplier;

    public float wisdomBonus;
    public float wisdomMultiplier;

    public float physicalDefenseBonus;
    public float physicalDefenseMultiplier;

    public float magicDefenseBonus;
    public float magicDefenseMultiplier;

    public float damageBonus;
    public float damageMultiplier;

    public float magicDamageBonus;
    public float magicDamageMultiplier;
}
