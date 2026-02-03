using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEditor;
using UnityEngine.TextCore.Text;

public class MovementTypeMeleeCombat : MovementType
{
    /*public Transform playerModel;
    public GameObject walkCam;
    public bool strafe;
    public Transform handPos;

    private float attackTime;
    private int comboIndex;

    public override void OnMovementEnter()
    {
        if(!handPos) { handPos = movement.player.GetComponent<PlayerCharacter>().equipment.handPos; }
        handPos.gameObject.SetActive(true);
        movement.animator.SetBool("Strafe", false);
        strafe = true;
    }

    private void Start()
    {
        movement.attackable = true;
    }

    public override void Tick()
    {
        //movement.animator.SetFloat("DirectionX", movement.moveAxis.x, 0.1f, Time.deltaTime);
        //movement.animator.SetFloat("DirectionY", movement.moveAxis.y, 0.1f, Time.deltaTime);
        Gravity();
        SlopeSliding();
        LockOnMovement();
        RotatePlayerModel();

        if (Time.deltaTime - attackTime > 2f)
        {
            comboIndex = 0;
        }
    }

    public void LockOnMovement()
    {
        Vector3 direction = new Vector3(movement.moveAxis.x, 0, movement.moveAxis.y);
        direction = direction.x * movement.cam.right.normalized + direction.z * movement.cam.forward.normalized;
        direction.y = 0;

        movement.animator.SetFloat("Speed", direction.magnitude);

        Vector3 lookDirection = movement.cam.position - playerModel.transform.position;
        lookDirection = new Vector3(lookDirection.x, 0, lookDirection.z);
        lookDirection.Normalize();

        movement.transform.rotation = Quaternion.Slerp(movement.transform.rotation, Quaternion.LookRotation(-lookDirection), 10 * Time.deltaTime);

        if (movement.moveAxis.magnitude == 0 || movement.freezeMovement)
        {
            return;
        }

        movement.controller.Move(direction.normalized * movement.speed * Time.deltaTime);
    }


    public override void Gravity()
    {
        if (movement.freezeGravity == false)
        {
            movement.grounded = Physics.CheckSphere(movement.groundCheck.position, movement.groundDistance, movement.groundMask);
            movement.animator.SetBool("Grounded", movement.grounded);

            if (movement.grounded && movement.velocity.y < 0)
            {
                movement.velocity.y = -2f;
            }

            movement.controller.Move(movement.velocity * Time.deltaTime);
            movement.velocity.y += movement.gravity * Time.deltaTime;
        }
    }

    private void SlopeSliding()
    {
        if (!movement.grounded)
        {
            if (Physics.SphereCast(movement.slopeCheck.position, movement.controller.radius - 0.05f, Vector3.down, 
                out var hit, 1f, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore))
            {
                var collider = hit.collider;
                var angle = Vector3.Angle(Vector3.up, hit.normal);

                if(angle > movement.controller.slopeLimit)
                {
                    var normal = hit.normal;
                    var yInverse = 1f - normal.y;
                    movement.velocity.x += yInverse * normal.x;
                    movement.velocity.z += yInverse * normal.z;
                } 
            }
        }

        else
        {
            movement.velocity.z = 0;
            movement.velocity.x = 0;
        }
    }

    public void RotatePlayerModel()
    {
        Vector3 direction = new Vector3(movement.moveAxis.x, 0, movement.moveAxis.y).normalized;
        if (strafe) { direction.x = 0; direction.z = 1; }

        if (movement.freezeMovement == false)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + movement.cam.eulerAngles.y;
            float angel = Mathf.SmoothDampAngle(playerModel.transform.eulerAngles.y, targetAngle, ref movement.turnVelocity, movement.turnSpeed);
            playerModel.transform.rotation = Quaternion.Euler(0, angel, 0);
        }
    }


    public override void Sprint(bool context)
    {
        if (context && movement.grounded)
        {
            movement.animator.SetBool("Sprint", true);
            movement.speed += movement.sprint;
            strafe = false;
            movement.animator.SetBool("Strafe", strafe);
        }

        if (!context)
        {
            movement.animator.SetBool("Sprint", false);
            movement.speed = movement.baseSpeed;
            strafe = true;
            movement.animator.SetBool("Strafe", strafe);
        }
    }


    public override void Crouch()
    {
        if (movement.grounded && !movement.crouching)
        {
            movement.animator.SetBool("Crouch", true);
            movement.controller.height = 1.25f;
            movement.controller.center = new Vector3(0, 0.6f, 0);
            movement.crouching = true;
        }

        else if(movement.grounded && !movement.crouched && movement.crouching)
        {
            movement.animator.SetBool("Crouch", false);
            movement.controller.height = 2;
            movement.controller.center = new Vector3(0, 1, 0);
            movement.crouching = false;
        }
    }

    public override void Attack(InputAction.CallbackContext context)
    {
        if (movement.grounded && movement.combatMode && movement.attackable && !movement.player.dead && !movement.dashing && movement.player.character.stats.CheckStaminaCost(10))
        {
            movement.attackable = false;
            movement.animator.SetTrigger("Attack" + comboIndex);
            WeaponObject weaponObject = movement.player.character.equipment.rightHandItem as WeaponObject;
            weaponObject.CalculateDamage();

            comboIndex++;

            if (comboIndex > 3)
            {
                comboIndex = 0;
            }
        }
    }

    public override void Jump()
    {
        if (movement.grounded && !movement.dashing && !movement.freezeJump)
        {
            movement.animator.SetTrigger("Jump");
            movement.velocity.y = Mathf.Sqrt(movement.jumpHeight * -2 * movement.gravity);
        }
    }

    public override void OnMovementExit()
    {
        movement.animator.SetBool("Strafe", false);
    }*/
}
