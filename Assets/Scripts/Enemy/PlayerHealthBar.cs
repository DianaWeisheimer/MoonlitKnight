using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : HealthBar
{
    public Slider staminaSlider;
    public Slider greenSlider;
    public Slider manaSlider;
    public Slider blueSlider;

    public override void FixedUpdate()
    {
        if (chip && redSlider.value >= healthSlider.value) { redSlider.value -= 0.15f; }
        if (redSlider.value <= healthSlider.value) { redSlider.value = healthSlider.value; }

        if (chip && greenSlider.value >= staminaSlider.value) { greenSlider.value -= 0.15f; }
        if (greenSlider.value <= staminaSlider.value) { greenSlider.value = staminaSlider.value; }

        if (chip && blueSlider.value >= manaSlider.value) { blueSlider.value -= 0.15f; }
        if (blueSlider.value <= manaSlider.value) { blueSlider.value = manaSlider.value; }
    }
    public override void UpdateHealthBar(CharacterStats stats)
    {
        healthSlider.maxValue = stats.maxHealth;
        redSlider.maxValue = healthSlider.maxValue;
        healthSlider.value = stats.health;

        staminaSlider.maxValue = stats.maxStamina;
        greenSlider.maxValue = staminaSlider.maxValue;
        staminaSlider.value = stats.stamina;

        manaSlider.maxValue = stats.maxMana;
        blueSlider.maxValue = manaSlider.maxValue;
        manaSlider.value = stats.mana;
    }

    public override void UpdateDamageText(float damageTaken)
    {
        damageText.text = damageTaken.ToString();
    }
}
