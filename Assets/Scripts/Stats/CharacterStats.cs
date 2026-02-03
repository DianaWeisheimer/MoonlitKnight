using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType { Health, Mana, Stamina, Vitality, Endurance, Arcane, Strength, Dexterity, Intelligence, Wisdom, Faith, 
    PhysicalDefense, MagicalDefense, FireDefense, LightingDefense, DivineDefense, OccultDefense,
    Poise, PoiseRecovery, Weight }

[System.Serializable]
public class CharacterStats
{
    public List<ItemStatBonus> itemStatBonus;
    public Character character;

    [SerializeField]
    private int level;

    private int xp;

    public LevelingSystem levelingSystem;

    public BaseStat baseStat;
    public event Action Died;
    public event Action UpdatedStats;

    public Dictionary<StatType, Stat> stats = new();

    public Stat GetStat(StatType type)
    {
        if (stats.TryGetValue(type, out var stat))
            return stat;

        Debug.LogWarning($"Stat {type} not found.");
        return null;
    }
    public int GetLevel() => level;
    public int GetXP() => xp;
    public void AddXP(int amount) => xp += amount;
    public bool SpendXP(int amount)
    {
        if (xp < amount) return false;
        xp -= amount;
        return true;
    }
    public void SetLevel(int value)
    {
        level = value;
    }
    public void SetXP(int value)
    {
        xp = value;
    }


    public void Initialize(Character character)
    {
        this.character = character;
        levelingSystem = new LevelingSystem(this);
        if (itemStatBonus == null)
            itemStatBonus = new List<ItemStatBonus>();

        stats = new Dictionary<StatType, Stat>
    {
        { StatType.Health, new Stat() },
        { StatType.Mana, new Stat() },
        { StatType.Stamina, new Stat() },
        { StatType.Vitality, new Stat() },
        { StatType.Endurance, new Stat() },
        { StatType.Arcane, new Stat() },
        { StatType.Strength, new Stat() },
        { StatType.Dexterity, new Stat() },
        { StatType.Intelligence, new Stat() },
        { StatType.Wisdom, new Stat() },
        { StatType.Faith, new Stat() },
        { StatType.PhysicalDefense, new Stat() },
        { StatType.MagicalDefense, new Stat() },
        { StatType.FireDefense, new Stat() },
        { StatType.LightingDefense, new Stat() },
        { StatType.DivineDefense, new Stat() },
        { StatType.OccultDefense, new Stat() },
        { StatType.Poise, new Stat() },
        { StatType.PoiseRecovery, new Stat() },
    };

        // Now apply the base values from your BaseStat SO
        stats[StatType.Health].SetBaseValue(baseStat.baseHealth);
        stats[StatType.Mana].SetBaseValue(baseStat.baseMana);
        stats[StatType.Stamina].SetBaseValue(baseStat.baseStamina);
        stats[StatType.Vitality].SetBaseValue(baseStat.baseVitality);
        stats[StatType.Endurance].SetBaseValue(baseStat.baseEndurance);
        stats[StatType.Arcane].SetBaseValue(baseStat.baseArcane);
        stats[StatType.Strength].SetBaseValue(baseStat.baseStrength);
        stats[StatType.Dexterity].SetBaseValue(baseStat.baseDexterity);
        stats[StatType.Intelligence].SetBaseValue(baseStat.baseIntelligence);
        stats[StatType.Wisdom].SetBaseValue(baseStat.baseWisdom);
        stats[StatType.Faith].SetBaseValue(baseStat.baseFaith);

        stats[StatType.PhysicalDefense].SetBaseValue(baseStat.basePhysicalDefense);
        stats[StatType.MagicalDefense].SetBaseValue(baseStat.magicDamage);
        stats[StatType.FireDefense].SetBaseValue(baseStat.magicDamage);
        stats[StatType.LightingDefense].SetBaseValue(baseStat.magicDamage);
        stats[StatType.DivineDefense].SetBaseValue(baseStat.magicDamage);
        stats[StatType.OccultDefense].SetBaseValue(baseStat.magicDamage);

        stats[StatType.Health].isDerived = true;
        stats[StatType.Mana].isDerived = true;
        stats[StatType.Stamina].isDerived = true;
        stats[StatType.Poise].isDerived = true;
        stats[StatType.PoiseRecovery].isDerived = true;

        CalculateDerivedStats();
        SetCurrentValue();
    }

    public void SetCurrentValue()
    {
        stats[StatType.Health].ResetCurrentValue();
        stats[StatType.Mana].ResetCurrentValue();
        stats[StatType.Stamina].ResetCurrentValue();
    }

    public void CalculateDerivedStats()
    {
        float vitality = stats[StatType.Vitality].baseValue + stats[StatType.Vitality].investedPoints;
        float endurance = stats[StatType.Endurance].baseValue + stats[StatType.Endurance].investedPoints;
        float strength = stats[StatType.Strength].baseValue + stats[StatType.Strength].investedPoints;
        float dexterity = stats[StatType.Dexterity].baseValue + stats[StatType.Dexterity].investedPoints;

        stats[StatType.Health].SetBaseValue(
            baseStat.baseHealth + vitality * 25
        );

        stats[StatType.Stamina].SetBaseValue(
            baseStat.baseStamina + endurance * 15
        );

        stats[StatType.Poise].SetBaseValue(
            baseStat.basePoise + strength * 2
        );

        stats[StatType.PoiseRecovery].SetBaseValue(
            baseStat.basePoiseRecovery + dexterity * 15
        );
    }


    public void Recharge()
    {
        stats[StatType.Health].Heal(stats[StatType.Health].maxValue * 0.0001f * (stats[StatType.Vitality].maxValue / 100));
        stats[StatType.Mana].Heal(stats[StatType.Mana].maxValue * 0.0002f);
        stats[StatType.Stamina].Heal(stats[StatType.Stamina].maxValue * 0.002f);
        stats[StatType.Poise].Heal(stats[StatType.PoiseRecovery].maxValue * 0.002f);

        UpdatedStats?.Invoke();
    }

    public void CalculateItemBonuses(Character character)
    {
        ItemStatBonus bonus = new ItemStatBonus();
        bonus.AddBonus(character.inventory.rightHand.GetStatBonuses());

        stats[StatType.Health].AddBonus(bonus.health);
        stats[StatType.Mana].AddBonus(bonus.mana);
        stats[StatType.Stamina].AddBonus(bonus.stamina);

        stats[StatType.Vitality].AddBonus(bonus.vitality);
        stats[StatType.Arcane].AddBonus(bonus.arcane);
        stats[StatType.Endurance].AddBonus(bonus.endurance);

        stats[StatType.Strength].AddBonus(bonus.strength);
        stats[StatType.Dexterity].AddBonus(bonus.dexterity);

        stats[StatType.Wisdom].AddBonus(bonus.wisdom);
        stats[StatType.Intelligence].AddBonus(bonus.intellect);
        stats[StatType.Faith].AddBonus(bonus.faith);

        stats[StatType.PhysicalDefense].AddBonus(bonus.physicalDefense);
        stats[StatType.MagicalDefense].AddBonus(bonus.magicalDefense);
        stats[StatType.FireDefense].AddBonus(bonus.magicalDefense);
        stats[StatType.LightingDefense].AddBonus(bonus.magicalDefense);
        stats[StatType.DivineDefense].AddBonus(bonus.magicalDefense);
        stats[StatType.OccultDefense].AddBonus(bonus.magicalDefense);

        stats[StatType.Poise].AddBonus(bonus.poise);
        stats[StatType.PoiseRecovery].AddBonus(bonus.poiseRecovery);

        CalculateDerivedStats();
    }

    public delegate void DamageModifier(ref float[] damages, GameObject source);
    public event DamageModifier OnBeforeTakeDamage;

    public DamageStack TakeDamage(AttackStack attackStack)
    {
        if(attackStack.critEffects != null && attackStack.critEffects.Count > 0)
        {
            foreach(var effect in attackStack.critEffects)
            {
                effect.ApplyCritEffect(attackStack.attacker, character);
            }
        }

        float physical = attackStack.damages[0] - stats[StatType.PhysicalDefense].maxValue;
        float magical = attackStack.damages[1] - stats[StatType.MagicalDefense].maxValue;
        float fire = attackStack.damages[2] - stats[StatType.FireDefense].maxValue;
        float lightning = attackStack.damages[3] - stats[StatType.LightingDefense].maxValue;
        float divine = attackStack.damages[4] - stats[StatType.DivineDefense].maxValue;
        float occult = attackStack.damages[5] - stats[StatType.OccultDefense].maxValue;
        float trueDamage = attackStack.damages[6];

        float[] finalDamage = { physical, magical, fire, lightning, divine, occult, trueDamage };

        if (OnBeforeTakeDamage != null)
        {
            OnBeforeTakeDamage(ref attackStack.damages, attackStack.attacker.gameObject);
        }

        DamageStack stack = stats[StatType.Health].TakeDamage(finalDamage);
        stack.staggered = stats[StatType.Poise].Staggered(attackStack.poiseDamage);

        return stack;
    }

    public void Heal(float heal)
    {
        stats[StatType.Health].Heal(heal);
    }

    public void AbilityCost(AbilitySO ability)
    {
        stats[StatType.Health].Drain(ability.healthCost);
        stats[StatType.Mana].Drain(ability.manaCost);
        stats[StatType.Stamina].Drain(ability.staminaCost);

        UpdatedStats?.Invoke();
    }

    public bool CheckAbilityCost(AbilitySO ability)
    {
        if(stats[StatType.Health].currentValue >= ability.healthCost && stats[StatType.Mana].currentValue >= ability.manaCost 
            && stats[StatType.Stamina].currentValue >= ability.staminaCost)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public bool CheckStaminaCost(float cost)
    {
        if(stats[StatType.Stamina].currentValue >= cost)
        {
            stats[StatType.Stamina].currentValue -= cost;
            return true;
        }

        else
        {
            return false;
        }
    }

    public void Die()
    {
        Died?.Invoke();
    }
}
