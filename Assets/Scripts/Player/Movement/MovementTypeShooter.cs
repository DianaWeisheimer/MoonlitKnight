using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using Cinemachine;

public class MovementTypeShooter : MovementType
{
    public GravityHandler gravityHandler;

    public Transform raycastOrigin;
    public GameObject walkCam;
    public CinemachineFreeLook aimCam;
    public bool strafe;
    public bool aiming;
    public Transform aimTarget;
    //public MultiAimConstraint aimRig;

    public float gravity;
    public float groundDistance;
    public Transform groundCheck;
    public Transform slopeCheck;
    public LayerMask groundMask;

    public override void TickUpdate()
    {
        gravityHandler.Tick();
    }


    public override void Tick()
    {
        //movement.animator.SetFloat("DirectionX", movement.moveAxis.x, 0.1f, Time.deltaTime);
        //movement.animator.SetFloat("DirectionY", movement.moveAxis.y, 0.1f, Time.deltaTime);

        ShootRaycast();
        RotatePlayerModel();
        Movement();
    }

    public override void Movement()
    {
        Vector3 direction = new Vector3(movement.moveAxis.x, 0, movement.moveAxis.y);
        direction = direction.x * movement.cam.right.normalized + direction.z * movement.cam.forward.normalized;
        direction.y = 0;

        movement.animator.SetFloat("Speed", direction.magnitude);

        Vector3 lookDirection = movement.cam.position - movement.playerModel.transform.position;
        lookDirection = new Vector3(lookDirection.x, 0, lookDirection.z);
        lookDirection.Normalize();

        movement.transform.rotation = Quaternion.Slerp(movement.transform.rotation, Quaternion.LookRotation(-lookDirection), 10 * Time.deltaTime);

        if (movement.moveAxis.magnitude == 0 || movement.freezeMovement)
        {
            return;
        }

        movement.controller.Move(direction.normalized * movement.speed * Time.deltaTime);
    }

    private void ShootRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            Vector3 hitPosition = hit.point;
            aimTarget.position = hitPosition;
            aimTarget.position = Vector3.MoveTowards(aimTarget.position, hitPosition, 200 * Time.deltaTime);

            Vector3 lookDirection = hitPosition - movement.transform.position;
            lookDirection = new Vector3(lookDirection.x, 0, lookDirection.z);
            lookDirection.Normalize();
        }
    }

    public void RotatePlayerModel()
    {
        Vector3 direction = new Vector3(movement.moveAxis.x, 0, movement.moveAxis.y).normalized;
        if (strafe) { direction.x = 0; direction.z = 1; }

        if (movement.freezeMovement == false)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + movement.cam.eulerAngles.y;
            float angel = Mathf.SmoothDampAngle(movement.playerModel.transform.eulerAngles.y, targetAngle, ref movement.turnVelocity, movement.turnSpeed);
            movement.playerModel.transform.rotation = Quaternion.Euler(0, angel, 0);
        }
    }

    public override void RightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            aimCam.m_Lens.FieldOfView = 35;
            aiming = true;
            //aimRig.weight = 1;
        }

        if (context.canceled)
        {
            aimCam.m_Lens.FieldOfView = 65;
            aiming = false;
            //aimRig.weight = 0;
        }
    }

    public override void Sprint(bool context)
    {
        if (context && gravityHandler.grounded)
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
        if (gravityHandler.grounded && !movement.crouching)
        {
            movement.animator.SetBool("Crouch", true);
            movement.controller.height = 1.25f;
            movement.controller.center = new Vector3(0, 0.6f, 0);
            movement.crouching = true;
        }

        else if(gravityHandler.grounded && !movement.crouched && movement.crouching)
        {
            movement.animator.SetBool("Crouch", false);
            movement.controller.height = 2;
            movement.controller.center = new Vector3(0, 1, 0);
            movement.crouching = false;
        }
    }

    public override void OnMovementEnter()
    {
        aimCam.gameObject.SetActive(true);
        movement.animator.SetBool("Strafe", false);
        strafe = true;
        aiming = false;
        //aimRig.weight = 0;
        aimCam.m_Lens.FieldOfView = 65;
        gravityHandler = new GravityHandler(gravity, movement.controller, groundCheck, groundDistance, slopeCheck, groundMask, movement.animator);
    }

    public override void OnMovementExit()
    {
        movement.animator.SetBool("Strafe", false);
        aimCam.gameObject.SetActive(false);
        //aimRig.weight = 0;
    }
}
