using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    //Components
    public Player player;
    public PlayerCharacter character;
    public CharacterController controller;
    public AnimationEvents animationEvents;
    public Animator animator;
    public Transform playerModel;

    public MovementType activeMovement;
    public MovementType[] movementTypes;
    public Transform cam;
    public static event Action<bool> OpenInventory;
    private bool inventoryOpened;
    public bool combatMode;
    public bool attackable;
    //Dash
    public float rollTime;
    public float rollSpeed;
    public float dashCooldown;
    public float startDashTime;
    public bool dashing;
    //Movement
    public float speed;
    public float baseSpeed;
    public float sprint;
    public bool sprinting;
    public float turnSpeed;
    public float turnVelocity;
    public Vector2 moveAxis;
    public Vector3 moveDir;
    public Transform rootTransform;
    //Jump/Gravity
    public bool freezeMovement;
    public bool freezeGravity;

    public float jumpHeight;
    public Transform slopeCheck;
    public bool crouching;
    public bool crouched;
    public bool freezeJump;

    public void Initialize(Player _player, PlayerCharacter _character, CharacterController _controller, AnimationEvents _animationEvents, Animator _animator)
    {
        player = _player;
        character = _character;
        controller = _controller;
        animationEvents = _animationEvents;
        animator = _animator;
        playerModel = animationEvents.transform;
        cam = Camera.main.transform;

        animationEvents.FreezeMovement += AttackStart;
        animationEvents.JumpObstacle += JumpOver;
        animationEvents.SwitchRightHand += SwitchRightHand;
        animationEvents.Attack += SetAttackable;
    }

    private void Start()
    {
        baseSpeed = speed;
        FreezeMovement(false);
        activeMovement.OnMovementEnter();
    }

    void Update()
    {
        if (Time.time > startDashTime + dashCooldown)
        {
            dashing = false;
        }

        animator.SetFloat("DirectionX", moveAxis.x, 0.1f, Time.deltaTime);
        animator.SetFloat("DirectionY", moveAxis.y, 0.1f, Time.deltaTime);
        animator.SetFloat("Direction", moveAxis.magnitude);

        activeMovement.TickUpdate();
    }

    private void FixedUpdate()
    {
        activeMovement.Tick();
        if (sprinting) character.stats.CheckStaminaCost(.55f);
    }

    public void FreezeMovement(bool hehe)
    {
        freezeMovement = hehe;
        freezeGravity = hehe;
    }

    private void AttackStart(bool hehe)
    {
        freezeMovement = hehe;
        freezeJump = hehe;
    }

    private void JumpOver(bool hehe)
    {
        freezeMovement = hehe;
        freezeJump = hehe;
        freezeGravity = hehe;
    }

    public void HandleInventory()
    {
        if (!character.dead)
        {
            OpenInventory?.Invoke(true);
            InputManager.instance.ChangeActionMap("UI");
            inventoryOpened = true;
        }
    }

    private void HandleAttack(InputAction.CallbackContext context)
    {
        activeMovement.Attack(context);
    }

    private void HandleMove(Vector2 axis)
    {
        moveAxis = axis;
    }

    private void HandleAbility()
    {
        character.abilityHolder.AbilityPressed();
    }

    public void HandleJump()
    {
        if (!character.dead && !dashing && character.stats.CheckStaminaCost(25))
        {
            activeMovement.Jump();
        }
    }

    public void HandleSprint(bool context)
    {
        activeMovement.Sprint(context);
    }

    public void HandleCrouch()
    {
        activeMovement.Crouch();
    }

    public void HandleEvade()
    {
        if(!dashing)
        {
            activeMovement.Evade();
        }
    }

    public void HandleLeftClick(bool context)
    {
        activeMovement.LeftClick(context);
    }

    public void HandleRightClick(InputAction.CallbackContext context)
    {
        activeMovement.RightClick(context);
    }

    public void HandleLockOnTarget()
    {
        activeMovement.LockOnTarget();
    }
    public void HandleSwitchLockOnTarget()
    {
        activeMovement.SwitchLockOnTarget();
    }

    public void HandleConsume()
    {
        activeMovement.Consume();
    }

    public void HandleSwitchHeldItem()
    {
        if(character.dead == false && !dashing)
        {
            animator.SetTrigger("SwitchHand");
        }
    }
    public void SwitchRightHand()
    {
        CheckMovementType();
    }

    public void HandleSwitchConsumable()
    {
        //if (player.dead == false && !dashing)
        //{
            //player.character.inventory.SwitchConsumable();
        //}
    }

    public void CheckMovementType()
    {
        if(combatMode)
        {
            combatMode = false;
            ChangeMovementType(0);
            return;
        }

        else if (!combatMode && ValidateCombatMode())
        {
            combatMode = true;
        }
    }

    private bool ValidateCombatMode()
    {
        bool validated = false;

        if(character.job.jobName == "Rogue" || character.job.jobName == "Knight")
        {
            validated = character.inventory.rightHand.item != null && character.inventory.leftHand.item != null;
        }

        return validated;
    }

    public void ChangeMovementType(int i)
    {
        activeMovement.OnMovementExit();
        activeMovement = movementTypes[i];
        activeMovement.OnMovementEnter();
    }

    public void SetAttackable(bool hehe)
    {
        attackable = hehe;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onAttackPressed += HandleAttack;
        GameEventsManager.instance.inputEvents.onMovePressed += HandleMove;
        GameEventsManager.instance.inputEvents.onSprintPressed += HandleSprint;
        GameEventsManager.instance.inputEvents.onEvadePressed += HandleEvade;
        GameEventsManager.instance.inputEvents.onAbilityPressed += HandleAbility;
        GameEventsManager.instance.inputEvents.onInventoryPressed += HandleInventory;
        GameEventsManager.instance.inputEvents.onJumpPressed += HandleJump;
        GameEventsManager.instance.inputEvents.onCrouchPressed += HandleCrouch;
        GameEventsManager.instance.inputEvents.onSwitchLockOnPressed += HandleSwitchLockOnTarget;
        GameEventsManager.instance.inputEvents.onLockOnPressed += HandleLockOnTarget;
        GameEventsManager.instance.inputEvents.onChangeRightHandPressed += HandleSwitchHeldItem;
        GameEventsManager.instance.inputEvents.onChangeConsumablePressed += HandleSwitchConsumable;
        GameEventsManager.instance.inputEvents.onRightClickPressed += HandleRightClick;
        GameEventsManager.instance.inputEvents.onConsumePressed += HandleConsume;
        
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onAttackPressed -= HandleAttack;
        GameEventsManager.instance.inputEvents.onMovePressed -= HandleMove;
        GameEventsManager.instance.inputEvents.onSprintPressed -= HandleSprint;
        GameEventsManager.instance.inputEvents.onEvadePressed -= HandleEvade;
        GameEventsManager.instance.inputEvents.onAbilityPressed -= HandleAbility;
        GameEventsManager.instance.inputEvents.onInventoryPressed -= HandleInventory;
        GameEventsManager.instance.inputEvents.onJumpPressed -= HandleJump;
        GameEventsManager.instance.inputEvents.onCrouchPressed -= HandleCrouch;
        GameEventsManager.instance.inputEvents.onSwitchLockOnPressed -= HandleSwitchLockOnTarget;
        GameEventsManager.instance.inputEvents.onLockOnPressed -= HandleLockOnTarget;
        GameEventsManager.instance.inputEvents.onChangeRightHandPressed -= HandleSwitchHeldItem;
        GameEventsManager.instance.inputEvents.onChangeConsumablePressed -= HandleSwitchConsumable;
        GameEventsManager.instance.inputEvents.onRightClickPressed -= HandleRightClick;
        GameEventsManager.instance.inputEvents.onConsumePressed -= HandleConsume;

        animationEvents.FreezeMovement -= AttackStart;
        animationEvents.JumpObstacle -= JumpOver;
        animationEvents.SwitchRightHand -= SwitchRightHand;
        animationEvents.Attack -= SetAttackable;
    }
}
