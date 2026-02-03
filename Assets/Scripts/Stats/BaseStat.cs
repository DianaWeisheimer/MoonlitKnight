using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBaseStat", menuName = "Base Stats")]
public class BaseStat : ScriptableObject
{
    //public List<float> baseStat = new List<float>(20);

    public float baseHealth;
    public float baseMana;
    public float baseStamina;
    public float baseVitality;
    public float baseEndurance;
    public float baseArcane;
    public float baseStrength;
    public float baseDexterity;
    public float baseIntelligence;
    public float baseWisdom;
    public float baseFaith;
    public float basePhysicalDefense;
    public float baseMagicDefense;
    public float basePoise;
    public float basePoiseRecovery;
    public float baseMinDamage;
    public float baseMaxDamage;
    public float magicDamage;
}
