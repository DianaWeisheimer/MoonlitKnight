using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image playerIcon;
    public Slider heathSlider;
    public Slider manaSlider;
    public Slider staminaSlider;
    public Character character;

    public void SetBar(PlayerCharacter playerCharacter)
    {
        playerIcon.sprite = playerCharacter.memberIcon;

        heathSlider.maxValue = playerCharacter.stats.GetStat(StatType.Health).maxValue;
        heathSlider.value = playerCharacter.stats.GetStat(StatType.Health).currentValue;

        manaSlider.maxValue = playerCharacter.stats.GetStat(StatType.Mana).maxValue;
        manaSlider.value = playerCharacter.stats.GetStat(StatType.Mana).currentValue;

        staminaSlider.maxValue = playerCharacter.stats.GetStat(StatType.Stamina).maxValue;
        staminaSlider.value = playerCharacter.stats.GetStat(StatType.Stamina).currentValue;
    }

    public void SetValue(CharacterStats stats)
    {
        character = stats.character;
        heathSlider.maxValue = stats.GetStat(StatType.Health).maxValue;
        heathSlider.value = stats.GetStat(StatType.Health).currentValue;

        manaSlider.maxValue = stats.GetStat(StatType.Mana).maxValue;
        manaSlider.value = stats.GetStat(StatType.Mana).currentValue;

        staminaSlider.maxValue = stats.GetStat(StatType.Stamina).maxValue;
        staminaSlider.value = stats.GetStat(StatType.Stamina).currentValue;
    }
}
