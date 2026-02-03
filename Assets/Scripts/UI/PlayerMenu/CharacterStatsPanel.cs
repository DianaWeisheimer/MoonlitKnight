using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Rendering;
public class CharacterStatsPanel : MonoBehaviour
{
    public List<TextMeshProUGUI> statTexts;
    public void RefreshInformation(CharacterStats stats)
    {
        statTexts[0].text = "Health: " + stats.GetStat(StatType.Health).maxValue.ToString();
        statTexts[1].text = "Mana: " + stats.GetStat(StatType.Mana).maxValue.ToString();
        statTexts[2].text = "Stamina: " + stats.GetStat(StatType.Stamina).maxValue.ToString();
        statTexts[3].text = "Vitality: " + stats.GetStat(StatType.Vitality).maxValue.ToString();
        statTexts[4].text = "Arcane: " + stats.GetStat(StatType.Arcane).maxValue.ToString();
        statTexts[5].text = "Endurance: " + stats.GetStat(StatType.Endurance).maxValue.ToString();
        statTexts[6].text = "Strength: " + stats.GetStat(StatType.Strength).maxValue.ToString();
        statTexts[7].text = "Dexterity: " + stats.GetStat(StatType.Dexterity).maxValue.ToString();
        statTexts[8].text = "Wisdom: " + stats.GetStat(StatType.Wisdom).maxValue.ToString();
        statTexts[9].text = "Intelligence: " + stats.GetStat(StatType.Intelligence).maxValue.ToString();
        statTexts[10].text = "P. Defense: " + stats.GetStat(StatType.PhysicalDefense).maxValue.ToString();
        statTexts[11].text = "M. Defense: " + stats.GetStat(StatType.MagicalDefense).maxValue.ToString();
    }
}
