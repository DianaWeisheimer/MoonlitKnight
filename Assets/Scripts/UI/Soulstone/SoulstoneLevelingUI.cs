using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulstoneLevelingUI : MonoBehaviour
{
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI newLevelText;
    public TextMeshProUGUI currentXPText;
    public TextMeshProUGUI newtXPText;
    public TextMeshProUGUI XPCostText;
    public List<SoulstoneLevelingSlider> levelingSliders;
    private LevelUpDraft currentDraft;
    private CharacterStats stats;
    public void SetUI()
    {
        stats = PartyManager.instance.GetActiveMember().core.character.stats;

        LevelUpDraft draft = new LevelUpDraft
        {
            startingLevel = stats.GetLevel(),
            startingRunes = stats.GetXP(),
            currentLevel = stats.GetLevel(),
            remainingRunes = stats.GetXP()
        };

        foreach (var stat in stats.stats.Keys)
            draft.pendingStatIncreases[stat] = 0;

        currentDraft = draft;
    
        levelingSliders[0].SetInitialValues(StatType.Vitality, stats.GetStat(StatType.Vitality));
        levelingSliders[1].SetInitialValues(StatType.Arcane, stats.GetStat(StatType.Endurance));
        levelingSliders[2].SetInitialValues(StatType.Endurance, stats.GetStat(StatType.Arcane));
        levelingSliders[3].SetInitialValues(StatType.Strength, stats.GetStat(StatType.Strength));
        levelingSliders[4].SetInitialValues(StatType.Dexterity, stats.GetStat(StatType.Dexterity));
        levelingSliders[5].SetInitialValues(StatType.Wisdom, stats.GetStat(StatType.Wisdom));
        levelingSliders[6].SetInitialValues(StatType.Intelligence, stats.GetStat(StatType.Intelligence));
        levelingSliders[7].SetInitialValues(StatType.Faith, stats.GetStat(StatType.Faith));

        RefreshUI();
    }

    public void TryIncreaseStat(StatType stat)
    {
        int cost = stats.levelingSystem.GetRuneCostForNextLevel(currentDraft.currentLevel);

        if (currentDraft.remainingRunes < cost)
            return;

        currentDraft.remainingRunes -= cost;
        currentDraft.currentLevel++;

        currentDraft.pendingStatIncreases[stat]++;

        currentDraft.levelHistory.Add(new DraftLevelEntry
        {
            stat = stat,
            runeCost = cost
        });

        RefreshUI();
    }

    public void TryDecreaseStat(StatType stat)
    {
        if (currentDraft.pendingStatIncreases[stat] <= 0)
            return;

        // Find the last increase for this stat
        for (int i = currentDraft.levelHistory.Count - 1; i >= 0; i--)
        {
            if (currentDraft.levelHistory[i].stat != stat)
                continue;

            DraftLevelEntry entry = currentDraft.levelHistory[i];

            currentDraft.remainingRunes += entry.runeCost;
            currentDraft.currentLevel--;

            currentDraft.pendingStatIncreases[stat]--;

            currentDraft.levelHistory.RemoveAt(i);
            RefreshUI();
            return;
        }
    }

    public void ConfirmLevelUp()
    {
        foreach (var entry in currentDraft.levelHistory)
        {
            stats.levelingSystem.LevelUpStat(entry.stat);
            stats.CalculateDerivedStats();
            stats.SetCurrentValue();
        }

        //DataPersistenceManager.instance.SaveGame();
    }

    private void RefreshUI()
    {
        currentLevelText.text = currentDraft.startingLevel.ToString();
        newLevelText.text = currentDraft.currentLevel.ToString();
        currentXPText.text = currentDraft.startingRunes.ToString();
        newtXPText.text = currentDraft.remainingRunes.ToString();
        XPCostText.text = stats.levelingSystem.GetRuneCostForNextLevel(currentDraft.currentLevel).ToString();

        foreach (var slider in levelingSliders)
        {
            int pending = currentDraft.pendingStatIncreases[slider.statType];
            slider.RefreshValues(pending);
        }
    }
}
