using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoulstoneLevelingSlider : MonoBehaviour
{
    public SoulstoneLevelingUI levelingUI;
    public TextMeshProUGUI statName;
    public TextMeshProUGUI currentLevel;
    public TextMeshProUGUI nextLevel;
    public StatType statType;
    public List<Color32> statColors;

    public void SetInitialValues(StatType type, Stat stat)
    {
        statType = type;
        statName.text = type.ToString();

        float currentStatLevel = stat.baseValue + stat.investedPoints;
        currentLevel.text = currentStatLevel.ToString();
        nextLevel.text = currentStatLevel.ToString();
    }

    public void RefreshValues(int pendingIncrease)
    {
        if(pendingIncrease > 0)
        {
            nextLevel.color = statColors[1];
        }

        else
        {
            nextLevel.color = statColors[0];
        }

        float nextStatLevel =
            float.Parse(currentLevel.text) + pendingIncrease;

        nextLevel.text = nextStatLevel.ToString();
    }

    public void IncreaseLevel()
    {
        levelingUI.TryIncreaseStat(statType);
    }

    public void DecreaseLevel()
    {
        levelingUI.TryDecreaseStat(statType);
    }
}
