using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStat", menuName = "EnemyStat")]
public class BaseCharacterStats : ScriptableObject
{
    public float maxHealth;
    public float health;
    public float vitality;
    public float stamina;
    public float endurance;
    public float mana;
    public float wisdom;
    public int defense;
    public int attack;
    public int dexterity;
    public int spirit;
}
