using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrainSpectralKnight : AIBrain
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

    private void FixedUpdate()
    {
        if (combatState.currentSubState is EnemyCombatAttack)
        {
            List<AttackStack> attacks = sensor.ScanForAttack();

            for(int i = 0; i < attacks.Count; i++)
            {
                if (attacks[i].attacker.type == CharacterType.Player)
                {
                    float distance = Vector3.Distance(transform.position, attacks[i].transform.position);
                    if (distance < 4.0f)
                        combatState.ChangeSubState(combatState.GetSubStates(CombatSubStateGroupID.Block)[0]);
                }
            }
        }
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

        // Character-level combat feedback ? converted into signals
        character.equipment.onBlockSuccess += () =>
        {
            combatState.EmitCombatSignal(SubstateSignal.BlockSuccess);
        };
    }

    /* ------------------- HIGH LEVEL STATE FLOW ------------------- */

    public override void PlayerFound()
    {
        ChangeState(combatState);
        var blocks = combatState.GetSubStates(CombatSubStateGroupID.Block);
        var ChosenBlock = blocks[0];
        combatState.ChangeSubState(ChosenBlock);
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
            case SubstateSignal.BlockSuccess:
                var counters = combatState.GetSubStates(CombatSubStateGroupID.Counter);
                var chosenCounter = counters[0];
                combatState.ChangeSubState(chosenCounter);
                break;

            case SubstateSignal.BlockTimeout:
                var attacks = combatState.GetSubStates(CombatSubStateGroupID.Attack);
                var chosenAttack = attacks[0];
                combatState.ChangeSubState(chosenAttack);
                break;

            case SubstateSignal.CounterTimeout:
                combatState.ChangeSubState(combatState.GetSubStates(CombatSubStateGroupID.Block)[0]);
                break;

            case SubstateSignal.AttackTimeout:
                var blocks = combatState.GetSubStates(CombatSubStateGroupID.Block);
                var ChosenBlock = blocks[0];
                combatState.ChangeSubState(ChosenBlock);
                break;

            case SubstateSignal.CloseRangeAttack:
                combatState.ChangeSubState(combatState.GetSubStates(CombatSubStateGroupID.Counter)[0]);
                break;

            case SubstateSignal.BackstabOpportunity:
                if(combatState.BackstabCooldown() == true)
                {
                    combatState.ChangeSubState(combatState.GetSubStates(CombatSubStateGroupID.Counter)[1]);
                }
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
        var weapons = character.inventory.GetWeaponsByCategory(WeaponCategory.Sword);
        var shields = character.inventory.GetWeaponsByCategory(WeaponCategory.Shield);

        if (weapons != null && weapons.Count > 0)
            character.EquipItem(weapons[0], EquipSlots.RightHand);

        if (shields != null && shields.Count > 0)
            character.EquipItem(shields[0], EquipSlots.LeftHand);
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

