using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementClimb : PlayerMovementType
{
    public GameObject ladder;
    public override void Gravity()
    {
            movement.grounded = Physics.CheckSphere(movement.groundCheck.position, movement.groundDistance, movement.groundMask);
            movement.animator.SetBool("Grounded", movement.grounded);
        /*if (movement.freezeGravity == false)
        {

            if (movement.grounded && movement.velocity.y < 0)
            {
                movement.velocity.y = -2f;
            }

            movement.controller.Move(movement.velocity * Time.deltaTime);
            movement.velocity.y += movement.gravity * Time.deltaTime;
        }*/
    }



    public override void Movement()
    {
        movement.transform.rotation = ladder.transform.rotation;
        Vector3 direction = new Vector3(0, movement.moveAxis.y, 0);
        Vector3 xdirection = new Vector3(movement.moveAxis.x, 0, 0);
        xdirection = xdirection.x * movement.transform.right;
        direction = direction + xdirection;

        movement.controller.Move(direction * movement.speed * 0.5f * Time.deltaTime);

        movement.animator.SetFloat("Speed", direction.magnitude);
        /*Vector3 direction = new Vector3(movement.moveAxis.x, 0, movement.moveAxis.y).normalized;


        if (direction.magnitude == 0)
        {
            movement.animator.SetBool("Sprint", false);
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
        }*/
    }



    public override void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed && movement.grounded)
        {
            movement.speed += movement.sprint;
            movement.animator.SetBool("Sprint", true);
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
        /*if (context.performed && movement.grounded && !movement.dashing && !movement.freezeJump)
        {
            movement.animator.SetTrigger("Jump");
            movement.velocity.y = Mathf.Sqrt(movement.jumpHeight * -2 * movement.gravity);
            movement.player.DrainStamina(-2);
            //jumpSource.Play();
        }*/
    }



    public override void Roll(InputAction.CallbackContext context)
    {
        /*if (context.performed && movement.grounded && !movement.dashing && !movement.freezeMovement)
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

            movement.animator.SetTrigger("Roll");
            movement.dashing = true;

            float startTime = Time.time;
            movement.startDashTime = Time.time;

            while (Time.time < startTime + movement.rollTime)
            {
                movement.controller.Move(movement.moveDir * movement.rollSpeed * Time.deltaTime);

                yield return null;
            }
        }*/
    }



    public override void Meditate()
    {
        if (movement.grounded && !movement.dashing && movement.moveAxis.magnitude == 0 && !movement.meditating)
        {
            movement.animator.SetTrigger("Meditate");
        }
    }
}
