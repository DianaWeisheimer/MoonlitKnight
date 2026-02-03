using System.Collections.Generic;
using UnityEngine;

public class LevelUpDraft
{
    public int startingLevel;
    public int startingRunes;

    public int currentLevel;
    public int remainingRunes;

    public Dictionary<StatType, int> pendingStatIncreases = new();
    public List<DraftLevelEntry> levelHistory = new();
}

public class DraftLevelEntry
{
    public StatType stat;
    public int runeCost;
}
