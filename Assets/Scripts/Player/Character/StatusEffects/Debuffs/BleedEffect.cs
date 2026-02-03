using UnityEngine;

public class BleedEffect : StatusEffect
{
    private float damagePerTick;
    private int stackLimit;
    private int stack;
    public override string EffectID => "Bleed";
    public override StackBehavior StackBehavior => StackBehavior.AddIntensity;
    public BleedEffect(float duration, float tickInterval, Character attacker, Character target, float damage, int stackLimit)
        : base(duration, tickInterval, attacker, target)
    {
        damagePerTick = damage;
        this.stackLimit = stackLimit;
    }

    public override void Tick()
    {
        float[] damages = { 0, 0, 0, 0, 0, 0, damagePerTick };

        AttackStack stack = new AttackStack();
        stack.SetValues(attacker, DamageType.True, damages);

        DamageStack damageStack = target.stats.TakeDamage(stack);
        if(damageStack.died == false) DamagePopupGenerator.instance.CreatePopup(target.transform.position, damageStack.damageTaken);
        Debug.Log($"Bleed deals {damagePerTick} to {target.name}");
    }

    protected override void AddIntensityFrom(StatusEffect other)
    {
        if (other is BleedEffect bleed && stack <= stackLimit)
        {
            stack++;
            damagePerTick += bleed.damagePerTick;
            Duration = Mathf.Max(Duration, bleed.Duration);
        }
    }

    protected override void CopyFrom(StatusEffect other)
    {
        if (other is BleedEffect bleed)
        {
            damagePerTick = bleed.damagePerTick;
        }
    }
}
