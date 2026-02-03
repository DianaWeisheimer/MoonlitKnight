using UnityEngine;

public class CompanionRoleProfile : ScriptableObject
{
    public float protectPlayerWeight;
    public float attackEnemyWeight;
    public float healPlayerWeight;
    public float stayCloseWeight;

    public virtual void Initialize(CompanionBrain brain)
    {

    }

    public virtual AIState DecideCombatAction(CompanionBrain brain)
    {
        // default behavior
        return null;
    }

    public virtual AICombatSubState DecideCombatSubstate(CompanionBrain brain, SubstateSignal signal)
    {
        // default behavior
        return null;
    }

    public virtual void ExecuteEngage(CompanionBrain brain)
    {
        
    }

    public virtual Enemy GetTarget()
    {
        return null;
    }
}
