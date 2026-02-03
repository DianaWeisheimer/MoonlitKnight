using System;
using UnityEngine;

public enum SubstateSignal
{
    None,
    AttackTimeout,
    BlockSuccess, BlockTimeout,
    TookDamage
    ,
    CloseRangeAttack, CounterTimeout,
    StrafeTimeout,
    BackstabOpportunity,
    PlayerDisengage,
}
public abstract class AICombatSubState : ScriptableObject
{
    protected EnemyStateCombat combatState;
    protected AIBrain brain;
    protected Animator animator;

    public event System.Action<SubstateSignal> Signal;

    public virtual void Initialize(EnemyStateCombat state)
    {
        combatState = state;
        brain = state.brain;
        animator = brain.characterModel.animator;
    }

    protected void Emit(SubstateSignal signal)
    {
        Signal?.Invoke(signal);
    }

    public abstract void OnEnter();
    public abstract void Tick();
    public abstract void OnExit();
}


