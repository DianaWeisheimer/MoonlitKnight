using UnityEngine;

public class MarkedForDeathEffect : StatusEffect
{
    public override string EffectID => "Mark";
    public override StackBehavior StackBehavior => StackBehavior.Replace;

    private float bonusMultiplier;
    private bool isConsumed = false;

    public MarkedForDeathEffect(float duration, Character attacker, Character target, float bonusMultiplier = 0.1f)
        : base(duration, 0f, attacker, target) // No tick interval needed
    {
        this.bonusMultiplier = bonusMultiplier;
        target.stats.OnBeforeTakeDamage += OnBeforeTakeDamage;
    }

    private void OnBeforeTakeDamage(ref float[] damages, GameObject source)
    {
        if (!isConsumed)
        {
            float bonus = damages[0] * bonusMultiplier;
            damages[0] += bonus;
            isConsumed = true;

            target.stats.OnBeforeTakeDamage -= OnBeforeTakeDamage;
            target.statusEffects.RemoveEffect(this);
        }
    }

    public override void Tick() { } // Nothing to tick

    public override bool Update()
    {
        if (isConsumed)
            return true;

        return base.Update();
    }

    public override void StackWith(StatusEffect other)
    {
        base.StackWith(other);
        isConsumed = false;
        target.stats.OnBeforeTakeDamage -= OnBeforeTakeDamage;
        target.stats.OnBeforeTakeDamage += OnBeforeTakeDamage;
    }
}
