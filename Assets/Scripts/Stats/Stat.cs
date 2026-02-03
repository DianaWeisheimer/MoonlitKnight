using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    //public StatType statType;
    public float baseValue;
    private StatBonus itemBonus;
    public int investedPoints;
    public float modifier; 
    public float multiplier;
    public float maxValue;
    public float currentValue;
    public bool isDerived;
    public event Action<Stat> onStatChange;

    public void IncreaseStat(int amount = 1)
    {
        investedPoints += amount;
        CalculateMaxValue();
    }

    public float SetBaseValue(float value)
    {
        baseValue = value;  
        onStatChange?.Invoke(this);
        CalculateMaxValue();
        return baseValue;
    }

    public void AddBonus(StatBonus statBonus)
    {
        itemBonus = statBonus;
        CalculateMaxValue();
    }

    public void ResetCurrentValue()
    {
        currentValue = maxValue;
    }

    public float CalculateMaxValue()
    {
        modifier = 0;
        multiplier = 0;
        if (itemBonus != null)
        {
            modifier += itemBonus.modifier;
            multiplier += itemBonus.multiplier;
        }

        maxValue = baseValue;

        if (!isDerived)
        {
            maxValue += investedPoints;
        }

        maxValue += modifier;
        maxValue *= (multiplier + 1);

        if (maxValue < 0) { maxValue = 0; }
        if (currentValue > maxValue) { currentValue = maxValue; }

        return maxValue;
    }

    public DamageStack TakeDamage(float[] damage)
    {
        float totalDamage = damage[0] + damage[1] + damage[2] + damage[3] + damage[4] + damage[5] + damage[6];
        currentValue -= totalDamage;

        DamageStack stack = new DamageStack();
        if(currentValue <= 0) { stack.died = true; }
        stack.damageTaken = damage;
        stack.totalDamage = totalDamage;
        stack.percentHealthLost = totalDamage * 100 / maxValue;
        stack.currentValue = currentValue;
        stack.percentHealth = currentValue * 100 / maxValue;

        return stack;
    }

    public bool Staggered(float damage)
    {
        currentValue -= damage;

        if (currentValue > 0)
        {
            return false;
        }

        else
        {
            currentValue = maxValue;
            return true;
        }
    }

    public void Heal(float heal)
    {
        currentValue += heal;
        if(currentValue > maxValue) { currentValue = maxValue; }
        onStatChange?.Invoke(this);
    }

    public void Drain(float drain)
    {
        currentValue -= drain;
        onStatChange?.Invoke(this);
    }
}
