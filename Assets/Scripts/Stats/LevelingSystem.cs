using UnityEngine;

[System.Serializable]
public class LevelingSystem
{
    private CharacterStats stats;

    public LevelingSystem(CharacterStats stats)
    {
        this.stats = stats;
    }

    public int GetNextLevelCost()
    {
        return GetRuneCostForNextLevel(stats.GetLevel());
    }

    public int GetRuneCostForNextLevel(int currentLevel)
    {
        return Mathf.FloorToInt(
            100 + Mathf.Pow(currentLevel, 1.5f) * 20
        );
    }

    public bool CanLevelUp()
    {
        return stats.GetXP() >= GetNextLevelCost();
    }

    public bool LevelUpStat(StatType stat)
    {
        int cost = GetNextLevelCost();
        if (!stats.SpendXP(cost))
            return false;

        stats.stats[stat].IncreaseStat(1);
        stats.SetLevel(stats.GetLevel() + 1);

        stats.CalculateDerivedStats();
        stats.SetCurrentValue();

        return true;
    }
}

