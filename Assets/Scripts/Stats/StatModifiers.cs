using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatModifiers
{
    public float healthModifier;
    public float healthMultiplier;

    public float manaModifier;
    public float manaMultiplier;

    public float staminaModifier;
    public float staminaMultiplier;

    public float damageModifier;
    public float damageMultiplier;

    public List<StatModifier> modifiers;

    public void CalculateModifiers()
    {
        healthModifier = 0;
        healthMultiplier = 0;

        manaModifier = 0;
        manaMultiplier = 0;

        staminaModifier = 0;
        staminaMultiplier = 0;

        damageModifier = 0;
        damageMultiplier = 0;

        for (int i = 0; i < modifiers.Count; i++)
        {
            healthModifier += modifiers[i].healthModifier;
            healthMultiplier += modifiers[i].healthMultiplier; 

            manaModifier += modifiers[i].manaModifier;
            manaMultiplier += modifiers[i].manaMultiplier;

            staminaModifier += modifiers[i].staminaModifier;
            staminaMultiplier += modifiers[i].staminaMultiplier;

            damageModifier += modifiers[i].damageModifier;
            damageMultiplier += modifiers[i].damageMultiplier;
        }   
    }

    public void AddModifier(StatModifier statModifier)
    {
        modifiers.Add(statModifier);
        CalculateModifiers();
    }

    public void RemoveModifier(StatModifier statModifier)
    {
        modifiers.Remove(statModifier);
        CalculateModifiers();
    }
}
