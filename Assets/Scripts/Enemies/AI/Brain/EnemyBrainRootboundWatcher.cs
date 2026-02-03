using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrainRootboundWatcher : AIBrain
{
    public EnemyStatePatrol stateScoutArea;
    public EnemyStateWander stateWander;
    public EnemyStateCombat combatState;
    public EnemyStateSearchPlayer stateSearchPlayer;
    public EnemyStateStunned stateStunned;
    public AttackStack[] stack;

    private void Start()
    {
        ChangeState(stateScoutArea);
        AssignEvents();
    }

    public override void ResetBrain()
    {
        ChangeState(stateScoutArea);
        AssignEvents();
    }

    private void AssignEvents()
    {
        characterModel.animationEvents.WeaponCollider += SetWeaponCollider;
        characterModel.animationEvents.Attack += OnAttack;
    }

    public override void PlayerFound()
    {
        ChangeState(combatState);
        var attacks = combatState.GetSubStates(CombatSubStateGroupID.Attack);
        var ChosenAttack = attacks[0];
        combatState.ChangeSubState(ChosenAttack);
        combatState.lookAtPlayer = true;
    }

    public override void PlayerLost()
    {
        stateSearchPlayer.startPosition = combatState.attackTarget.transform.position;
        ChangeState(stateSearchPlayer);
    }

    public override void OnPlayerDied()
    {
        ChangeState(stateScoutArea);
    }

    public void NoScoutArea()
    {
        ChangeState(stateWander);
    }

    public override void PlayerNotFound()
    {
        ChangeState(stateScoutArea);
    }

    public void FinishedStun()
    {
        stateSearchPlayer.startPosition = transform.position;
        ChangeState(stateSearchPlayer);
    }

    public void OnTakeHit()
    {
        if(currentState == stateWander || currentState == stateScoutArea || currentState == stateSearchPlayer)
        {
            ChangeState(stateStunned);
        }
    }

    public void SetWeaponCollider(bool hehe, bool rightHand)
    {
        stack = character.characterModel.GetComponentsInChildren<AttackStack>();

        if(stack == null || stack.Length < 2)
        {
            return;
        }

        if (rightHand && stack[1])
        {
            stack[1].SetValues(character, DamageType.Physical, CalculateDamage());
            stack[1].GetComponent<BoxCollider>().enabled = hehe;
        }

        else if(!rightHand && stack[0])
        {
            stack[0].SetValues(character, DamageType.Physical, CalculateDamage());
            stack[0].GetComponent<BoxCollider>().enabled = hehe;
        }

        if (!hehe)
        {
            combatState.lookAtPlayer = false;
        }
    }

    public void OnAttack(bool hehe)
    {
        agent.isStopped = !hehe;

        if (hehe)
        {
            combatState.lookAtPlayer = false;
        }
    }

    private float[] CalculateDamage()
    {
        float physicalDamage = 1 * (character.stats.GetStat(StatType.Strength).maxValue * 10);
        float magicDamage = 1 * (character.stats.GetStat(StatType.Wisdom).maxValue * 10);
        float divineDamage = 1 * (character.stats.GetStat(StatType.Faith).maxValue * 10);
        float occultDamage = 1 * (character.stats.GetStat(StatType.Intelligence).maxValue * 10);
        if(magicDamage == 1) { magicDamage = 0; }
        if(divineDamage == 1) { divineDamage = 0; }
        if(occultDamage == 1) { occultDamage = 0; }

        float[] damages = { physicalDamage, magicDamage, 0, 0, divineDamage, occultDamage, 0 };

        return damages;
    }

    /* ------------------- COMBAT SIGNAL HANDLING ------------------- */

    public override void OnCombatSignal(SubstateSignal signal)
    {
        switch (signal)
        {
            case SubstateSignal.CounterTimeout:
                combatState.ChangeSubState(combatState.GetSubStates(CombatSubStateGroupID.Attack)[0]);
                break;

            case SubstateSignal.CloseRangeAttack:
                combatState.ChangeSubState(combatState.GetSubStates(CombatSubStateGroupID.Counter)[0]);
                break;

            case SubstateSignal.AttackTimeout:
                combatState.ChangeSubState(combatState.GetSubStates(CombatSubStateGroupID.Attack)[0]);
                break;
        }
    }

    private void OnEnable()
    {
        stateScoutArea.NoScoutArea += NoScoutArea;
        stateSearchPlayer.PlayerNotFound += PlayerNotFound;
        stateStunned.FinishedStun += FinishedStun;
    }

    private void OnDisable()
    {
        stateScoutArea.NoScoutArea -= NoScoutArea;
        stateSearchPlayer.PlayerNotFound -= PlayerNotFound;
        stateStunned.FinishedStun -= FinishedStun;
        characterModel.animationEvents.WeaponCollider -= SetWeaponCollider;
        characterModel.animationEvents.Attack -= OnAttack;
    }
}
