using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputEvents
{
    public event Action<Vector2> onMovePressed;
    public void MovePressed(Vector2 moveDir)
    {
        if (onMovePressed != null)
        {
            onMovePressed(moveDir);
        }
    }

    public event Action<bool> onSprintPressed;
    public void SprintPressed(bool context)
    {
        if (onSprintPressed != null)
        {
            onSprintPressed(context);
        }
    }

    public event Action<InputAction.CallbackContext> onAttackPressed;
    public void AttackPressed(InputAction.CallbackContext context)
    {
        if (onAttackPressed != null)
        {
            onAttackPressed(context);
        }
    }

    public event Action onConsumePressed;
    public void ConsumePressed()
    {
        if (onConsumePressed != null)
        {
            onConsumePressed();
        }
    }

    public event Action onJumpPressed;
    public void JumpPressed()
    {
        if (onJumpPressed != null)
        {
            onJumpPressed();
        }
    }

    public event Action onInteractPressed;
    public void InteractPressed()
    {
        if (onInteractPressed != null)
        {
            onInteractPressed();
        }
    }

    public event Action onEvadePressed;
    public void EvadePressed()
    {
        if (onEvadePressed != null)
        {
            onEvadePressed();
        }
    }

    public event Action onAbilityPressed;
    public void AbilityPressed()
    {
        if (onAbilityPressed != null)
        {
            onAbilityPressed();
        }
    }

    public event Action onInventoryPressed;
    public void InventoryPressed()
    {
        if (onInventoryPressed != null)
        {
            onInventoryPressed();
        }
    }

    public event Action onChangeRightHandPressed;
    public void ChangeRightHandPressed()
    {
        if (onChangeRightHandPressed != null)
        {
            onChangeRightHandPressed();
        }
    }

    public event Action onChangeConsumablePressed;
    public void ChangeConsumablePressed()
    {
        if (onChangeConsumablePressed != null)
        {
            onChangeConsumablePressed();
        }
    }

    public event Action onChangeAbilityPressed;
    public void ChangeAbilityPressed()
    {
        if (onChangeAbilityPressed != null)
        {
            onChangeAbilityPressed();
        }
    }

    public event Action onLockOnPressed;
    public void LockOnPressed()
    {
        if (onLockOnPressed != null)
        {
            onLockOnPressed();
        }
    }

    public event Action onSwitchLockOnPressed;
    public void SwitchLockOnPressed()
    {
        if (onSwitchLockOnPressed != null)
        {
            onSwitchLockOnPressed();
        }
    }

    public event Action onCrouchPressed;
    public void CrouchPressed()
    {
        if (onCrouchPressed != null)
        {
            onCrouchPressed();
        }
    }

    public event Action<InputAction.CallbackContext> onRightClickPressed;
    public void RightClickPressed(InputAction.CallbackContext context)
    {
        if (onRightClickPressed != null)
        {
            onRightClickPressed(context);
        }
    }

    public event Action onChangeCharacterPressed;
    public void ChangeCharacterPressed()
    {
        if (onChangeCharacterPressed != null)
        {
            onChangeCharacterPressed();
        }
    }
}
