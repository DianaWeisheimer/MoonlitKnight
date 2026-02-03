using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStatusEffect
{
    public List<StatusEffect> activeEffects = new List<StatusEffect>();
    private Character character;

    public void Initialize(Character character)
    {
        this.character = character;
        // Optional: initialize with existing effects if needed
    }

    public void Update()
    {
        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            if (activeEffects[i].Update())
                activeEffects.RemoveAt(i);
        }
    }

    public void ApplyEffect(StatusEffect newEffect)
    {
        for (int i = 0; i < activeEffects.Count; i++)
        {
            if (activeEffects[i].EffectID == newEffect.EffectID)
            {
                activeEffects[i].StackWith(newEffect);
                return;
            }
        }

        activeEffects.Add(newEffect);
    }

    public void RemoveEffect(StatusEffect effect)
    {
        if (activeEffects.Contains(effect))
        {
            activeEffects.Remove(effect);
        }
    }
}
