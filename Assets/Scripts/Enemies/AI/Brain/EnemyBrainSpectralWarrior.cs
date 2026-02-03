using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrainSpectralWarrior : AIBrain
{
    [Header("States")]
    public EnemyStatePatrol stateScoutArea;
    public EnemyStateWander stateWander;
    public EnemyStateCombat combatState;
    public EnemyStateSearchPlayer stateSearchPlayer;
    public EnemyStateStunned stateStunned;

    private void Start()
    {
        ChangeState(stateScoutArea);
        AssignEvents();
        EquipWeapon();
    }

    public override void ResetBrain()
    {
        ChangeState(stateScoutArea);
        agent.isStopped = false;
        AssignEvents();
    }

    private void AssignEvents()
    {
        characterModel.animationEvents.Attack += OnAttack;
        characterModel.animationEvents.WeaponCollider += SetWeaponCollider;
        characterModel.animationEvents.block += SetBlock;
    }

    /* ------------------- HIGH LEVEL STATE FLOW ------------------- */

    public override void PlayerFound()
    {
        ChangeState(combatState);
        var attacks = combatState.GetSubStates(CombatSubStateGroupID.Attack);
        var ChosenAttack = attacks[0];
        combatState.ChangeSubState(ChosenAttack);
    }

    public override void PlayerLost()
    {
        stateSearchPlayer.startPosition = transform.position;
        ChangeState(stateSearchPlayer);
    }

    public override void PlayerNotFound()
    {
        ChangeState(stateScoutArea);
    }

    public override void OnPlayerDied()
    {
        ChangeState(stateScoutArea);
    }

    public override void OnTakeHit(DamageStack stack)
    {
        if (stack.staggered)
        {
            characterModel.animator.SetTrigger("Stagger");
            ChangeState(stateStunned);
        }
    }

    public void FinishedStun()
    {
        stateSearchPlayer.startPosition = transform.position;
        ChangeState(stateSearchPlayer);
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
                combatState.ChangeSubState(combatState.GetSubStates(CombatSubStateGroupID.Counter)[1]);
                break;
        }
    }

    /* ------------------- ANIMATION / EQUIPMENT ------------------- */

    public void OnAttack(bool active)
    {
        agent.isStopped = !active;
        combatState.lookAtPlayer = active;
    }

    public void SetWeaponCollider(bool active, bool rightHand)
    {
        character.equipment.CalculateWeaponDamage(1);
        character.equipment.SetWeaponCollider(active, rightHand);
    }

    public void SetBlock(bool rightHand, bool active)
    {
        character.equipment.SetWeaponBlock(rightHand, active);
    }

    private void EquipWeapon()
    {
        var weapons = character.inventory.GetWeaponsByCategory(WeaponCategory.Axe);

        if (weapons != null && weapons.Count > 0)
            character.EquipItem(weapons[0], EquipSlots.RightHand);
    }

    private void OnEnable()
    {
        stateScoutArea.NoScoutArea += () => ChangeState(stateWander);
        stateSearchPlayer.PlayerNotFound += PlayerNotFound;
        stateStunned.FinishedStun += FinishedStun;
    }

    private void OnDisable()
    {
        stateScoutArea.NoScoutArea -= () => ChangeState(stateWander);
        stateSearchPlayer.PlayerNotFound -= PlayerNotFound;
        stateStunned.FinishedStun -= FinishedStun;

        characterModel.animationEvents.Attack -= OnAttack;
        characterModel.animationEvents.WeaponCollider -= SetWeaponCollider;
        characterModel.animationEvents.block -= SetBlock;
    }
}
