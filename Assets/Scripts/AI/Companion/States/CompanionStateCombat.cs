using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompanionStateCombat : AIState
{
    [Header("Combat SubStates")]
    public List<CombatSubStateGroup> subStateGroups;

    public AICombatSubState currentSubState;

    public Enemy attackTarget;

    private Dictionary<CombatSubStateGroupID, List<AICombatSubState>> subStateMap;
    private CompanionBrain companionBrain;
    public bool lookAtTarget;

    public List<AICombatSubState> GetSubStates(CombatSubStateGroupID id)
    {
        if (subStateMap == null)
        {
            Debug.LogError("Combat substate map not initialized");
            return null;
        }

        if (subStateMap.TryGetValue(id, out var states))
        {
            return states;
        }

        Debug.LogWarning($"No combat substates found for group '{id}'");
        return null;
    }

    public override void OnStateEnter()
    {
        companionBrain = brain as CompanionBrain;
        companionBrain.role.Initialize(companionBrain);
    }

    public override void OnStateExit()
    {
        lookAtTarget = false;
    }

    public override void Tick()
    {
        companionBrain.role.ExecuteEngage(companionBrain);
        if(lookAtTarget) HandleRotation();
    }

    private void HandleRotation()
    {
        attackTarget = companionBrain.role.GetTarget();

        if (!attackTarget) return;

        Vector3 direction = attackTarget.transform.position - brain.agent.transform.position;
        Vector3 newDir = Vector3.RotateTowards(brain.agent.transform.forward, direction, 4 * Time.deltaTime, 0);
        newDir.y = 0;
        brain.agent.transform.rotation = Quaternion.LookRotation(newDir);
    }
}
