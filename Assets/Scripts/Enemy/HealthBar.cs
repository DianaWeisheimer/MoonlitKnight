using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider redSlider;
    public bool chip;
    public TextMeshProUGUI damageText;
    public Animator damageTextAnimator;

    public virtual void FixedUpdate()
    {
        if (chip && redSlider.value >= healthSlider.value)
        {
            redSlider.value -= 0.15f;
        }

        if (redSlider.value <= healthSlider.value) { redSlider.value = healthSlider.value; }
    }
    public virtual void UpdateHealthBar(CharacterStats stats)
    {
        healthSlider.maxValue = stats.maxHealth;
        redSlider.maxValue = healthSlider.maxValue;
        healthSlider.value = stats.health;
    }

    public virtual void UpdateDamageText(float damageTaken)
    {
        damageText.text = damageTaken.ToString();
    }
}
