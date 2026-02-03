using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class AIBrain : MonoBehaviour
{
    public Character character;
    public CharacterModel characterModel;
    public AISensor sensor;
    public ICombatActor combatActor;
    public NavMeshAgent agent;
    public AIState currentState;

    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        combatActor = GetComponentInParent<ICombatActor>();
        character = GetComponentInParent<Character>();
    }

    void Update()
    {
        characterModel?.animator.SetFloat("Speed", agent.velocity.magnitude);
        currentState?.Tick();
        Tick();
    }

    public void ConfigureBrain(List<AIBrainConfiguration> configuration)
    {
        if (configuration == null || configuration.Count <= 0) return;
        for (int i = 0; i < configuration.Count; i++)
        {
            configuration[i].ConfigureBrain(this);
        }
    }

    public virtual void Tick()
    {

    }

    public virtual void ResetBrain()
    {

    }

    public virtual GameObject AcquireTarget()
    {
        return sensor.ScanForPlayer().FirstOrDefault();
    }

    public virtual void OnCombatSignal(SubstateSignal signal)
    {

    }

    public void ChangeState(AIState newState)
    {
        if (currentState != newState)
        {
            if (currentState) currentState.OnStateExit();
            currentState = newState;
            currentState.OnStateEnter();
        }
    }

    public virtual void PlayerFound()
    {

    }

    public virtual void PlayerNotFound()
    {

    }

    public virtual void PlayerLost()
    {

    }

    public virtual void OnTakeHit(DamageStack stack)
    {

    }

    public virtual void OnPlayerDied()
    {
        
    }

    public void RunCoroutine(IEnumerator routine)
    {
        if(routine == null) return;
        if(gameObject.activeSelf == false) return;
        StartCoroutine(routine);
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying || currentState == null) return;

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.cyan;
        Handles.Label(transform.position + Vector3.up * 2,
            $"State: {currentState.name}", style);
    }
#endif
}
