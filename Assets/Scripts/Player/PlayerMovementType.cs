using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementType : MonoBehaviour
{
    public PlayerMovement movement;
    public Player player;
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

    public virtual void Jump(InputAction.CallbackContext context)
    {

    }

    public virtual void Sprint(InputAction.CallbackContext context)
    {

    }

    public virtual void Roll(InputAction.CallbackContext context)
    {

    }

    public virtual void Meditate()
    {

    }

    public virtual void Stance(InputAction.CallbackContext context)
    {

    }

    public virtual void LeftClick(InputAction.CallbackContext context)
    {

    }

    public virtual void OnMovementExit()
    {

    }
}
