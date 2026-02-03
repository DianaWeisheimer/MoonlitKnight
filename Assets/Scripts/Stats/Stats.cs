using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    public Stat health;
    public Stat mana;
    public Stat stamina;
    public Stat vitality;
    public Stat wisdom;
    public Stat endurance;
    public Stat defense;
    public Stat strength;
    public Stat dexterity;
    public Stat faith;
    public Stat darkness;
    public Stat minDamage;
    public Stat maxDamage;

    //public Stat damage;

    public void StartStats(BaseStat baseStat)
    {
        SetBaseStats(baseStat);
        SetCurrentValue();
    }

    public void SetBaseStats(BaseStat baseStat)
    {
        /*health.SetBaseValue(baseStat.baseHealth);
        mana.SetBaseValue(baseStat.baseMana);
        stamina.SetBaseValue(baseStat.baseStamina);
        minDamage.SetBaseValue(baseStat.baseMinDamage);
        maxDamage.SetBaseValue(baseStat.baseMaxDamage);*/
    }

    public void SetCurrentValue()
    {
        health.ResetCurrentValue();
        mana.ResetCurrentValue();
        stamina.ResetCurrentValue();
        minDamage.ResetCurrentValue();
        maxDamage.ResetCurrentValue();
    }
}
