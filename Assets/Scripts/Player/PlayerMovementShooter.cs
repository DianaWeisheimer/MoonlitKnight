using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerMovementShooter : PlayerMovementType
{
    public static event Action<bool> CrossHair;
    public float rotationSpeed;
    public GameObject gun;
    public Transform aimTarget;
    public Transform rayPos;
    public Rig rig;
    public bool aiming;
    public AnimatorOverrideController[] animations;


    public override void OnMovementEnter()
    {
        //AimRig(true);
        movement.cinemachineShooter.gameObject.SetActive(true);
        movement.animator.SetBool("Stance", true);
        SetAnimations(animations[0]);
    }

    public override void SetAnimations(AnimatorOverrideController animatorOverride)
    {
        movement.animatorOverrider.SetAnimations(animatorOverride);
    }

    public override void Tick()
    {
        RotateGun();
    }

    public void RotateGun()
    {
        RaycastHit hit;

        if(Physics.Raycast(rayPos.transform.position, movement.camShooter.transform.forward, out hit, Mathf.Infinity))
        {               
            Vector3 hitPosition = hit.point;
            aimTarget.position = hitPosition;
            //aimTarget.position = Vector3.MoveTowards(aimTarget.position, hitPosition, 200 * Time.deltaTime);

            Vector3 lookDirection = hitPosition - gun.transform.position;
            lookDirection.Normalize();

            if (!movement.shot && aiming)
            {
                //gun.transform.LookAt(hitPosition);
                gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, Quaternion.LookRotation(lookDirection), 24f * Time.deltaTime);
            }
        }
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


    public override void Movement()
    {
        if (aiming)
        {
            Vector3 direction = new Vector3(movement.moveAxis.x, 0, movement.moveAxis.y);
            direction = direction.x * movement.camShooter.right.normalized + direction.z * movement.camShooter.forward.normalized;
            direction.y = 0;

            movement.animator.SetFloat("Speed", direction.magnitude);
            Quaternion targetRotation = Quaternion.Euler(0, movement.camShooter.eulerAngles.y, 0);
            movement.transform.rotation = Quaternion.Lerp(movement.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (direction.magnitude == 0)
            {
                return;
            }

            if (movement.freezeMovement == false)
            {
                movement.controller.Move(direction * movement.speed * Time.deltaTime);
                movement.moveDir = direction;

            }
        }

        else if (!aiming)
        {
            Vector3 direction = new Vector3(movement.moveAxis.x, 0, movement.moveAxis.y).normalized;
            movement.animator.SetFloat("Speed", direction.magnitude);

            if (direction.magnitude == 0)
            {
                return;
            }

            if (movement.freezeMovement == false)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + movement.cam.eulerAngles.y;
                float angel = Mathf.SmoothDampAngle(movement.transform.eulerAngles.y, targetAngle, ref movement.turnVelocity, movement.turnSpeed);
                movement.transform.rotation = Quaternion.Euler(0, angel, 0);
                Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                movement.controller.Move(moveDirection.normalized * movement.speed * Time.deltaTime);
                movement.moveDir = moveDirection;
            }
        }
    }


    public override void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed && movement.grounded)
        {
            movement.animator.SetBool("Sprint", true);
            movement.speed += movement.sprint;
            movement.player.Sprint(true);
        }

        if (context.canceled)
        {
            movement.speed = movement.baseSpeed;
            movement.animator.SetBool("Sprint", false);
            movement.player.Sprint(false);
        }
    }


    public override void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && movement.grounded && !movement.dashing && !movement.freezeJump && !aiming)
        {
            movement.animator.SetTrigger("Jump");
            movement.velocity.y = Mathf.Sqrt(movement.jumpHeight * -2 * movement.gravity);
            movement.player.DrainStamina(-2);
            //jumpSource.Play();
        }
    }


    public override void Roll(InputAction.CallbackContext context)
    {
        if (context.performed && movement.grounded && !movement.dashing && !movement.freezeMovement && !aiming)
        {
            StartCoroutine(StartRoll());
        }

        else
        {
            return;
        }

        IEnumerator StartRoll()
        {
            player.DrainStamina(-2);

            movement.animator.SetTrigger("RollUpper");
            movement.animator.SetTrigger("Roll");
            movement.dashing = true;

            float startTime = Time.time;
            movement.startDashTime = Time.time;

            while (Time.time < startTime + movement.rollTime)
            {
                movement.controller.Move(movement.moveDir * movement.rollSpeed * Time.deltaTime);

                yield return null;
            }
        }
    }


    public override void Meditate()
    {
        if (movement.grounded && !movement.dashing && movement.moveAxis.magnitude == 0 && !movement.meditating)
        {
            movement.animator.SetTrigger("Meditate");
        }
    }

    public override void Stance(InputAction.CallbackContext context)
    {
        if (context.performed && movement.grounded && !movement.dashing)
        {
            if (aiming) 
            { 
                aiming = false; 
                movement.camShooter.gameObject.SetActive(false);
                SetAnimations(animations[0]);
                AimRig(false);
                CrossHair?.Invoke(false);
            }

            else 
            { 
                aiming = true; 
                movement.camShooter.gameObject.SetActive(true);
                SetAnimations(animations[1]);
                AimRig(true);
                CrossHair?.Invoke(true);
            }
        }
    }

    public override void LeftClick(InputAction.CallbackContext context)
    {
        if (context.performed && movement.grounded && !movement.dashing)
        {
            movement.cinemachineShooter.m_Lens.FieldOfView = 35f;
        }

        else if (context.canceled)
        {
            movement.cinemachineShooter.m_Lens.FieldOfView = 50f;
        }
    }

    public void AimRig(bool hehe)
    {
        if (hehe)
        {
            rig.weight = 1;
        }

        else
        {
            rig.weight = 0;
        }
    }

    public override void OnMovementExit()
    {
        AimRig(false);
        movement.cinemachineShooter.gameObject.SetActive(false);
        movement.animator.SetBool("Stance", false);
    }
}
