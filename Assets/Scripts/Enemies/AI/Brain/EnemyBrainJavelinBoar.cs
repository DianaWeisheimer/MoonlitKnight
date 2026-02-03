using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrainJavelinBoar : AIBrain
{
    public EnemyStatePatrol stateScoutArea;
    public EnemyStateWander stateWander;
    public EnemyStateCombat combatState;
    public EnemyStateSearchPlayer stateSearchPlayer;
    public EnemyStateStunned stateStunned;

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
    }

    public override void PlayerFound()
    {
        ChangeState(combatState);
        var attacks = combatState.GetSubStates(CombatSubStateGroupID.Attack);
        var ChosenAttack = attacks[0];
        combatState.ChangeSubState(ChosenAttack);
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
        AttackStack stack = GetComponent<AttackStack>();

        if (stack)
        {
            stack.SetValues(character, DamageType.Physical, CalculateDamage());
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

            case SubstateSignal.BackstabOpportunity:
                if (combatState.BackstabCooldown() == true)
                {
                    combatState.ChangeSubState(combatState.GetSubStates(CombatSubStateGroupID.Counter)[0]);
                }
                break;

            case SubstateSignal.AttackTimeout:
                combatState.ChangeSubState(combatState.GetSubStates(CombatSubStateGroupID.Attack)[0]);
                break;

            case SubstateSignal.PlayerDisengage:
                combatState.ChangeSubState(combatState.GetSubStates(CombatSubStateGroupID.Counter)[0]);
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
    }
}
