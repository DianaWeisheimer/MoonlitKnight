using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.TextCore.Text;

public class MovementTypeGround : MovementType
{
    public GravityHandler gravityHandler;
    public LockonHandler lockonHandler;
    public MouseTracker mouseTracker;
    public CombatHandler combatHandler;

    public GameObject crouchCamera;
    public Transform handPos;

    public float gravity;
    public float groundDistance;
    public Transform groundCheck;
    public Transform slopeCheck;
    public LayerMask groundMask;

    public CinemachineVirtualCamera lockonCamera;
    public float lockonRange;
    public LayerMask lockonLayerMask;

    public override void TickUpdate()
    {
        if (lockonHandler.lockedTarget) { LockOnMovement(); }
        else if (!lockonHandler.lockedTarget) { Movement(); }

        combatHandler.Tick();
        lockonHandler.Tick();
        gravityHandler.Tick();
        mouseTracker.Tick();
    }
    public override void Tick()
    {
        movement.animator.SetFloat("DirectionX", movement.moveAxis.x, 0.1f, Time.deltaTime);
        movement.animator.SetFloat("DirectionY", movement.moveAxis.y, 0.1f, Time.deltaTime);


        switch (movement.sprinting)
        {
            case true:
                movement.speed += 0.1f;
                if (movement.speed > movement.sprint) { movement.speed = movement.sprint; }
                break;
            case false:
                movement.speed -= 0.1f;
                if (movement.speed < movement.baseSpeed) { movement.speed = movement.baseSpeed; }
                break;
        }

        Vector2 mouseDir =
            new Vector2(mouseTracker.speedX, 0);


        if (mouseDir.sqrMagnitude > .01f)
        {
            lockonHandler.SwitchTargetByMouse(mouseDir.normalized);
        }
    }

    public override void Movement()
    {
        Vector3 direction = new Vector3(movement.moveAxis.x, 0, movement.moveAxis.y).normalized;

        if (direction.magnitude == 0 || movement.freezeMovement)
        {
            movement.animator.SetFloat("Speed", movement.controller.velocity.magnitude);
            return;
        }

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + movement.cam.eulerAngles.y;
        float angel = Mathf.SmoothDampAngle(movement.playerModel.transform.eulerAngles.y, targetAngle, ref movement.turnVelocity, movement.turnSpeed);
        movement.playerModel.transform.rotation = Quaternion.Euler(0, angel, 0);
        Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        movement.controller.Move(moveDirection.normalized * movement.moveAxis.magnitude * movement.speed * Time.deltaTime);
        movement.moveDir = moveDirection;
        movement.animator.SetFloat("Speed", movement.controller.velocity.magnitude);
    }

    public void LockOnMovement()
    {
        if(lockonHandler.lockOnTarget == null)
        {
            bool foundTarget = lockonHandler.FindLockOnTarget();

            if(foundTarget == false)
            {
                lockonHandler.UnlockTarget();
                return;
            }
        }

        if(Vector3.Distance(transform.position, lockonHandler.lockOnTarget.position) >= lockonHandler.lockOnRange *3)
        {
            lockonHandler.UnlockTarget();
            return;
        }

        Vector3 direction = new Vector3(movement.moveAxis.x, 0, movement.moveAxis.y);
        direction = direction.x * movement.cam.right.normalized + direction.z * movement.cam.forward.normalized;
        direction.y = 0;

        Vector3 lookDirection = lockonHandler.lockOnTarget.position - movement.transform.position;
        lookDirection = new Vector3(lookDirection.x, 0, lookDirection.z);
        lookDirection.Normalize();

        movement.playerModel.transform.rotation = Quaternion.Slerp(movement.playerModel.transform.rotation, Quaternion.LookRotation(lookDirection), 20 * Time.deltaTime);
        movement.transform.rotation = Quaternion.Slerp(movement.transform.rotation, Quaternion.LookRotation(lookDirection), 5 * Time.deltaTime);

        movement.animator.SetFloat("Speed", direction.magnitude);
        
        if (movement.moveAxis.magnitude == 0 || movement.freezeMovement)
        {
            return;
        }

        movement.controller.Move(direction.normalized * movement.speed * Time.deltaTime);
        movement.moveDir = direction;
    }

    public override void LockOnTarget()
    {
        lockonHandler.LockOnTarget();
    }

    public override void Sprint(bool context)
    {
        if (context && gravityHandler.grounded)
        {
            movement.sprinting = true; 
            movement.animator.SetBool("Sprint", true);
        }

        if (!context)
        {
            movement.sprinting = false;
            movement.animator.SetBool("Sprint", false);
        }
    }

    public override void Attack(InputAction.CallbackContext context)
    {
        if(context.performed && movement.attackable) combatHandler.Attack(false);
    }

    public override void RightClick(InputAction.CallbackContext context)
    {
        if (movement.character.job.jobName == "KK")
        {
            if (context.performed)
            {
                movement.freezeMovement = true;
                combatHandler.Guard(true);
            }

            else if (context.canceled)
            {
                movement.freezeMovement = false;
                combatHandler.Guard(false);
            }
        }

        else if(context.performed && movement.attackable) combatHandler.Attack(true);
    }

    public override void Consume()
    {
        //ConsumableItem item = movement.player.character.inventory.equippedConsumables[0].item as ConsumableItem;
        //movement.animator.SetTrigger(item.animationToPlay);
        /*InventorySlot item = PartyManager.instance.inventory.GetConsumable(0);
        ConsumableEffect effect = (item.item as ConsumableItem).effects[0];
        effect.Consume(movement.character);*/
        PartyManager.instance.inventory.UseConsumable(movement.character, 0);
    }

    public override void Crouch()
    {
        if (gravityHandler.grounded && !movement.crouching)
        {
            crouchCamera.SetActive(true);
            movement.animator.SetBool("Crouch", true);
            movement.controller.height = 1.25f;
            movement.controller.center = new Vector3(0, 0.6f, 0);
            movement.crouching = true;
        }

        else if(gravityHandler.grounded && !movement.crouched && movement.crouching)
        {
            crouchCamera.SetActive(false);
            movement.animator.SetBool("Crouch", false);
            movement.controller.height = 2;
            movement.controller.center = new Vector3(0, 1, 0);
            movement.crouching = false;
        }
    }

    public override void Jump()
    {
        if (gravityHandler.grounded && !movement.dashing && !movement.freezeJump)
        {
            StartCoroutine(JumpCharge());
        }
    }

    public IEnumerator JumpCharge()
    {
        movement.animator.SetTrigger("Jump");
        yield return new WaitForSeconds(0.15f);
        gravityHandler.velocity.y = Mathf.Sqrt(movement.jumpHeight * -2 * gravityHandler.gravity);
    }

    public override void Evade()
    {
        if(movement.character.stats.CheckStaminaCost(35))
        {
            StartCoroutine(StartRoll());

            IEnumerator StartRoll()
            {
                movement.freezeMovement = false;
                movement.animator.SetTrigger("Roll");
                movement.dashing = true;
                StartCoroutine(movement.player.IFrames(.25f));

                float startTime = Time.time;
                movement.startDashTime = Time.time;

                while (Time.time < startTime + movement.rollTime)
                {
                    movement.controller.Move(movement.moveDir * movement.rollSpeed * Time.deltaTime);

                    yield return null;
                }
            }
        }
    }

    public override void OnMovementEnter()
    {
        lockonHandler = new LockonHandler(movement, transform, lockonCamera, lockonRange, lockonLayerMask);
        gravityHandler = new GravityHandler(gravity, movement.controller, groundCheck, groundDistance, slopeCheck, groundMask, movement.animator);
        mouseTracker = new MouseTracker(Mouse.current.position.ReadValue());
        combatHandler = new CombatHandler(movement);
    }

    public override void OnMovementExit()
    {
        lockonHandler.UnlockTarget();
    }
}
