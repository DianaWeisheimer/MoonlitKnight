using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class MovementTypeClimb : MovementType
{
    /*public override void Tick()
    {
        Movement();
    }

    public override void Movement()
    {
        Vector2 direction = new Vector2(movement.moveAxis.x, movement.moveAxis.y).normalized;

        movement.animator.SetFloat("Speed", direction.magnitude);

        RaycastHit hit;

        if (Physics.Raycast(movement.transform.position, movement.transform.forward, out hit))
        {
            movement.transform.forward = -hit.normal;
            transform.rotation = Quaternion.FromToRotation(movement.transform.forward, hit.normal) * movement.transform.rotation;

            //movement.controller.transform.position
        }

        movement.controller.Move(transform.TransformDirection(direction) * movement.baseSpeed * Time.deltaTime);

        /*if (direction.magnitude == 0)
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

    public override void Sprint(bool context)
    {
        if (context && movement.grounded)
        {
            movement.animator.SetBool("Sprint", true);
            movement.speed += movement.sprint;
        }

        if (!context)
        {
            movement.speed = movement.baseSpeed;
            movement.animator.SetBool("Sprint", false);
        }
    }

    public override void Jump()
    {
        if (movement.grounded && !movement.dashing && !movement.freezeJump)
        {
            movement.animator.SetTrigger("Jump");

            movement.velocity.y = Mathf.Sqrt(movement.jumpHeight * -2 * movement.gravity);
        }
    }*/   
}
