using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementType : MonoBehaviour
{
    public PlayerMovement movement;
    //public Player player;
    private AnimatorOverrideController animatorOverride;

    public virtual void OnMovementEnter()
    {

    }
    public virtual void SetAnimations(AnimatorOverrideController animatorOverride)
    {

    }
    public virtual void Tick()
    {

    }

    public virtual void Movement()
    {

    }

    public virtual void Gravity()
    {

    }

    public virtual void Attack(InputAction.CallbackContext context)
    {

    }

    public virtual void Jump()
    {

    }

    public virtual void JumpObstacle()
    {

    }

    public virtual void Sprint(bool context)
    {

    }

    public virtual void Crouch()
    {

    }

    public virtual void Evade()
    {

    }

    public virtual void LeftClick(bool context)
    {

    }

    public virtual void RightClick(InputAction.CallbackContext context)
    {

    }

    public virtual void LockOnTarget()
    {

    }

    public virtual void SwitchLockOnTarget()
    {

    }

    public virtual void OnMovementExit()
    {

    }

    public virtual void Consume()
    {

    }

    public virtual void TickUpdate()
    {
        
    }
}
