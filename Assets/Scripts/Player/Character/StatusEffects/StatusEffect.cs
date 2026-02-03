using UnityEngine;

public enum StackBehavior
{
    RefreshDuration,
    AddDuration,
    AddIntensity,
    Replace,
    Ignore
}

public abstract class StatusEffect
{
    public float Duration { get; protected set; }
    public float TickInterval { get; protected set; }

    protected float nextTickTime;
    protected Character target;
    protected Character attacker;

    public abstract string EffectID { get; }

    public virtual StackBehavior StackBehavior => StackBehavior.RefreshDuration;

    public StatusEffect(float duration, float tickInterval, Character attacker, Character target)
    {
        Duration = duration;
        TickInterval = tickInterval;
        this.target = target;
        this.attacker = attacker;
        nextTickTime = Time.time + TickInterval;
    }

    public virtual bool Update()
    {
        Duration -= Time.deltaTime;

        if (Time.time >= nextTickTime)
        {
            nextTickTime = Time.time + TickInterval;
            Tick();
        }

        return Duration <= 0f;
    }

    public abstract void Tick();

    public virtual void StackWith(StatusEffect other)
    {
        switch (StackBehavior)
        {
            case StackBehavior.RefreshDuration:
                Duration = other.Duration;
                break;
            case StackBehavior.AddDuration:
                Duration += other.Duration;
                break;
            case StackBehavior.AddIntensity:
                AddIntensityFrom(other);
                break;
            case StackBehavior.Replace:
                Duration = other.Duration;
                CopyFrom(other);
                break;
            case StackBehavior.Ignore:
                // Do nothing
                break;
        }
    }

    protected virtual void AddIntensityFrom(StatusEffect other) { }
    protected virtual void CopyFrom(StatusEffect other) { }
}
