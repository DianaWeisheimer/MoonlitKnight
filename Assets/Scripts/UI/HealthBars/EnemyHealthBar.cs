using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : HealthBar
{
    public Slider slider;
    public Character character;
    private void Update()
    {  
        SetValue(character.stats.GetStat(StatType.Health));
    }
    public void SetValue(Stat stat)
    {
        slider.maxValue = stat.maxValue;
        slider.value = stat.currentValue;
    }
}
