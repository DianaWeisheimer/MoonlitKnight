using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementWater : PlayerMovementType
{
    public override void Gravity()
    {
        /*if (movement.freezeGravity == false)
        {
            movement.grounded = Physics.CheckSphere(movement.groundCheck.position, movement.groundDistance, movement.groundMask);
            movement.animator.SetBool("Grounded", movement.grounded);

            if (movement.grounded && movement.velocity.y < 0)
            {
                movement.velocity.y = -1f;
            }

            movement.velocity.y += movement.gravity * Time.deltaTime;
        }*/
            movement.controller.Move(movement.velocity * Time.deltaTime);
    }



    public override void Movement()
    {
        Vector3 direction = new Vector3(movement.moveAxis.x, 0, movement.moveAxis.y).normalized;

        movement.animator.SetFloat("Speed", direction.magnitude);

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
        }
    }



    public override void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movement.speed += movement.sprint;
            //movement.animator.SetBool("Sprint", true);
            movement.player.Sprint(true);
        }

        if (context.canceled)
        {
            movement.speed = movement.baseSpeed;
            //movement.animator.SetBool("Sprint", false);
            movement.player.Sprint(false);
        }
    }



    public override void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movement.velocity.y = 5;
        }

        if (context.canceled)
        {
            movement.velocity.y = 0;
        }
    }



    public override void Roll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movement.velocity.y = -5;
        }

        if (context.canceled)
        {
            movement.velocity.y = 0;
        }
    }



    public override void Meditate()
    {
        /*if (movement.grounded && !movement.dashing && movement.moveAxis.magnitude == 0 && !movement.meditating)
        {
            movement.animator.SetTrigger("Meditate");
        }*/
    }
}
