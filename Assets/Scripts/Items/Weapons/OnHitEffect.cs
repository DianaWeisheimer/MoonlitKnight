using UnityEngine;

[System.Serializable]
public abstract class OnHitEffect
{
    public abstract void ApplyCritEffect(Character attacker, Character target);
}
