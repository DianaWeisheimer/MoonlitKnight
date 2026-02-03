using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterStatsListDebugging : MonoBehaviour
{
    public Character character;
    public List<TextMeshProUGUI> statsText;

    private void Start()
    {
        character = PartyManager.instance.GetActiveMember().core.character;
    }
    void Update()
    {
        if(character != null)
        {
            statsText[0].text = "Health = " + character.stats.GetStat(StatType.Health).currentValue.ToString();
            statsText[1].text = "Mana = " + character.stats.GetStat(StatType.Mana).currentValue.ToString();
            statsText[2].text = "Stamina = " + character.stats.GetStat(StatType.Stamina).currentValue.ToString();
            statsText[3].text = "Vitality = " + character.stats.GetStat(StatType.Vitality).maxValue.ToString();
            statsText[4].text = "Endurance = " + character.stats.GetStat(StatType.Endurance).maxValue.ToString();
            statsText[5].text = "Arcane = " + character.stats.GetStat(StatType.Arcane).maxValue.ToString();
            statsText[6].text = "Strength = " + character.stats.GetStat(StatType.Strength).maxValue.ToString();
            statsText[7].text = "Dexterity = " + character.stats.GetStat(StatType.Dexterity).maxValue.ToString();
            statsText[8].text = "Intelligence = " + character.stats.GetStat(StatType.Intelligence).maxValue.ToString();
            statsText[9].text = "Wisdom = " + character.stats.GetStat(StatType.Wisdom).maxValue.ToString();
            statsText[10].text = "Faith = " + character.stats.GetStat(StatType.Faith).maxValue.ToString();
            statsText[11].text = "Current Poise = " + character.stats.GetStat(StatType.Poise).currentValue.ToString() + " Max Poise = " + character.stats.GetStat(StatType.Poise).maxValue.ToString();
            statsText[12].text = "PoiseRecovery = " + character.stats.GetStat(StatType.PoiseRecovery).maxValue.ToString();
        }
    }
}
