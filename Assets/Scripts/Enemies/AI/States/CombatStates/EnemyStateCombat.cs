using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStateCombat : AIState
{
    [Header("Combat SubStates")]
    public List<CombatSubStateGroup> subStateGroups;

    public AICombatSubState currentSubState;

    public GameObject attackTarget;
    public bool lookAtPlayer;

    [Header("Player Search")]
    public float losePlayerTime;
    public float lostPlayerTime;

    public float backstabRange = 5f;
    public float backstabCooldown = 4f;
    private float lastBackstabTime;

    private Dictionary<CombatSubStateGroupID, List<AICombatSubState>> subStateMap;

    public override void OnStateEnter()
    {
        subStateMap = subStateGroups.ToDictionary(
            g => g.id,
            g => g.states
        );
    }

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

    public override void Tick()
    {
        if(attackTarget == null) attackTarget = brain.AcquireTarget();
        currentSubState?.Tick();
        HandleRotation();

        if (brain.sensor.ScanForPlayer().Count > 0)
        {
            lostPlayerTime = Time.time;
        }

        if (Time.time - lostPlayerTime > losePlayerTime)
        {
            brain.PlayerLost();
        }
    }

    public override void OnStateExit()
    {
        currentSubState?.OnExit();
    }

    public void ChangeSubState(AICombatSubState newState)
    {
        currentSubState?.OnExit();

        currentSubState = Instantiate(newState);
        currentSubState.Initialize(this);

        // THIS is the connection
        currentSubState.Signal += EmitCombatSignal;

        currentSubState.OnEnter();
    }

    public bool CanBackstab(GameObject player)
    {
        Player model = player.GetComponent<Player>();

        if(model == false) return false;

        CharacterModel characterModel = model.character.characterModel;

        Vector3 toEnemy = (brain.transform.position - characterModel.transform.position).normalized;
        float facingDot = Vector3.Dot(characterModel.transform.forward, toEnemy);

        if (facingDot > -0.5f)
        {
            return false;
        }
        if (Vector3.Distance(brain.transform.position, player.transform.position) > backstabRange)
        {
            return false;
        }
        if (attackTarget == null)
        {
            return false;
        }

        return true;
    }


    public bool BackstabCooldown()
    {
        if (Time.time < lastBackstabTime + backstabCooldown)
            return false;
        else
        {
            lastBackstabTime = Time.time;
            return true;
        }
    }

    public void EmitCombatSignal(SubstateSignal signal)
    {
        brain.OnCombatSignal(signal);
    }

    private void HandleRotation()
    {
        if (!attackTarget || !lookAtPlayer) return;

        Vector3 direction = attackTarget.transform.position - brain.agent.transform.position;
        Vector3 newDir = Vector3.RotateTowards(brain.agent.transform.forward, direction, 4 * Time.deltaTime, 0);
        newDir.y = 0;
        brain.agent.transform.rotation = Quaternion.LookRotation(newDir);
    }
}



