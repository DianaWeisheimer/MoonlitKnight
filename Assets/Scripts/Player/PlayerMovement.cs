using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    //Components
    public PlayerMovementType activeMovement;
    public PlayerMovementType[] movementTypes;
    public Player player;
    public CharacterController controller;
    public Animator animator;
    public AnimatorOverrider animatorOverrider;
    public AnimatorOverrideController[] overrideControllers;
    public Transform cam;
    public Transform camShooter;
    public CinemachineFreeLook cinemachineFree;
    public CinemachineVirtualCamera cinemachineShooter;
    public PlayerInput playerInput;
    //Dash
    public float rollTime;
    public float rollSpeed;
    public float dashCooldown;
    public float startDashTime;
    public bool dashing;
    public bool meditating;
    //Movement
    public float speed;
    public float baseSpeed;
    public float sprint;
    public float turnSpeed;
    public float turnVelocity;
    public Vector3 moveDir;
    public Vector2 moveAxis;
    //Jump/Gravity
    public float jumpHeight;
    public float gravity;
    public float groundDistance;
    public Vector3 velocity;
    public Transform groundCheck;
    public LayerMask groundMask;
    public bool grounded;
    public bool freezeMovement;
    public bool freezeJump;
    public bool freezeGravity;
    public bool shot;
    //Inspection
    public List<Inspect> inspects;
    public Inspect nearestInstpect;
    float inspectDistance;
    float inspectNearestDistance = 10000;
    //Audio
    //public AudioSource walkSource;
    //public AudioSource jumpSource;
    //public AudioSource rollSource;
    //public AudioClip[] walkClip;
    //int clipToPlay;
    public void PlayWalkSound()
    {
        //clipToPlay = Random.Range(0, walkClip.Length);
        //walkSource.clip = walkClip[clipToPlay];
        //walkSource.Play();
    }

    private void Start()
    {
        baseSpeed = speed;
        shot = false;
    }

    void Update()
    {
        if (Time.time > startDashTime + dashCooldown)
        {
            dashing = false;
        }

        activeMovement.Gravity();
        activeMovement.Movement();
        activeMovement.Tick();
    }

    public void HandleInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player.inventoryController.OpenInventory();
            if (player.inventoryController.opened) { cinemachineFree.enabled = false; playerInput.SwitchCurrentActionMap("Menu"); }
            if (!player.inventoryController.opened) { cinemachineFree.enabled = true; playerInput.SwitchCurrentActionMap("Player"); }
        }
    }

    public void HandleInput(InputAction.CallbackContext context)
    {
        moveAxis = context.ReadValue<Vector2>();
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        activeMovement.Jump(context);
    }

    public void HandleSprint(InputAction.CallbackContext context)
    {
        activeMovement.Sprint(context);
    }
    
    public void HandleMeditate(InputAction.CallbackContext context)
    {
        activeMovement.Meditate();      
    }

    public void StartRoll(InputAction.CallbackContext context)
    {
        activeMovement.Roll(context);
    }

    public void HandleStance(InputAction.CallbackContext context)
    {
        activeMovement.Stance(context);
    }

    public void HandleLeftClick(InputAction.CallbackContext context)
    {
        activeMovement.LeftClick(context);
    }

    public void ChangeMovementType(int i)
    {
        activeMovement.OnMovementExit();
        activeMovement = movementTypes[i];
        animatorOverrider.SetAnimations(overrideControllers[i]);
        activeMovement.OnMovementEnter();
    }

    public void HandleInspect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(inspects != null)
            {
                for(int i = 0; i<inspects.Count; i++)
                {
                    inspectDistance = Vector3.Distance(transform.position, inspects[i].transform.position);

                    if(inspectDistance < inspectNearestDistance)
                    {
                        inspectNearestDistance = inspectDistance;
                        nearestInstpect = inspects[i];
                        nearestInstpect.StartInspect(true);
                    }
                }
            }
            //if (player.inventory.opened) { cinemachineFree.enabled = false; playerInput.SwitchCurrentActionMap("Inspect"); }
            //if (!player.inventory.opened) { cinemachineFree.enabled = true; playerInput.SwitchCurrentActionMap("Player"); }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            //animatorOverrider.SetAnimations(overrideControllers[1]);
            ChangeMovementType(1);
        }

        if (other.CompareTag("Ladder"))
        {
            //animatorOverrider.SetAnimations(overrideControllers[2]);
            ChangeMovementType(2);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water") || other.CompareTag("Ladder"))
        {
            animatorOverrider.SetAnimations(overrideControllers[0]);
            ChangeMovementType(0);
        }
    }

    /*public IEnumerator Dash(string anim, float dashTime, float dashSpeed)
    {
        //animator.SetTrigger(anim);
        dashing = true;
        freezeMovement = true;
        freezeJump = true;

        float startTime = Time.time;
        startDashTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            controller.Move(moveDir * dashSpeed * Time.deltaTime);

            yield return null;
        }

        freezeMovement = false;
        yield return new WaitForSeconds(0.5f);
        freezeJump = false;
    }*/
}
