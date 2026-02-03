using UnityEngine;

[System.Serializable]
public class BleedOnHitEffect : OnHitEffect
{
    public override void ApplyCritEffect(Character attacker, Character target)
    {
        var bleed = new BleedEffect(3, .5f, attacker, target, 5f, 4);
        target.statusEffects.ApplyEffect(bleed);
    }
}
