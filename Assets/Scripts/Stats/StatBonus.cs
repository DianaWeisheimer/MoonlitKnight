using System;
using UnityEngine;

[System.Serializable]
public class StatBonus
{
    public float modifier;
    public float multiplier;

    public StatBonus(float modifier, float multiplier)
    {
        this.modifier = modifier;
        this.multiplier = multiplier;
    }

    internal void Add(StatBonus bonus)
    {
        modifier += bonus.modifier;
        multiplier += bonus.multiplier;
    }
}
