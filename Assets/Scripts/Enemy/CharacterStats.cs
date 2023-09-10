using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth;
    public float health;

    public float maxStamina;
    public float stamina;

    public float maxMana;
    public float mana;

    public float maxVitality;
    public float vitality;

    public float maxEndurance;
    public float endurance;

    public float maxWisdom;
    public float wisdom;

    public int maxDefense;
    public int defense;

    public int maxAttack;
    public int attack;

    public int maxDexterity;
    public int dexterity;

    public int maxSpirit;
    public int spirit;

    public void LoadBaseStats(BaseCharacterStats baseStats)
    {
        maxHealth = baseStats.maxHealth;
        health = baseStats.health;

        maxStamina = baseStats.stamina;
        stamina = baseStats.stamina;

        mana = baseStats.mana;
        maxMana = baseStats.mana;

        maxVitality = baseStats.vitality;
        vitality = baseStats.vitality;

        maxEndurance = baseStats.endurance;
        endurance = baseStats.endurance;

        maxWisdom = baseStats.wisdom;
        wisdom = baseStats.wisdom;

        maxDefense = baseStats.defense;
        defense = baseStats.defense;

        maxAttack = baseStats.attack;
        attack = baseStats.attack;

        maxDexterity = baseStats.dexterity;
        dexterity = baseStats.dexterity;

        maxSpirit = baseStats.spirit;
        spirit = baseStats.spirit;
    }
}
