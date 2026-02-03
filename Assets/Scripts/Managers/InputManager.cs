using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }
    public PlayerInput input;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Party Manager in the scene.");
        }

        instance = this;
    }

    public void ChangeActionMap(string newActionMap)
    {
        input.SwitchCurrentActionMap(newActionMap);

        /*if(newActionMap == "UI")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }*/
    }

    //Player
    public void HandleMovement(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            GameEventsManager.instance.inputEvents.MovePressed(context.ReadValue<Vector2>());
        }
    }

    public void HandleSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.inputEvents.SprintPressed(true);
        }

        if (context.canceled)
        {
            GameEventsManager.instance.inputEvents.SprintPressed(false);
        }
    }

    public void HandleConsume(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.inputEvents.ConsumePressed();
        }
    }

    public void HandleRightClick(InputAction.CallbackContext context)
    {
        GameEventsManager.instance.inputEvents.RightClickPressed(context);
    }

    public void HandleInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.inputEvents.InteractPressed();
        }
    }

    public void HandleEvade(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.inputEvents.EvadePressed();
        }
    }

    public void HandleAbility(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.inputEvents.AbilityPressed();
        }
    }

    public void HandleInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.inputEvents.InventoryPressed();
        }
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.inputEvents.JumpPressed();
        }
    }

    public void HandleAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.inputEvents.AttackPressed(context);
        }
    }

    public void HandleCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.inputEvents.CrouchPressed();
        }
    }

    public void HandleLockOn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.inputEvents.LockOnPressed();
        }
    }

    public void HandleSwitchLockOn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.inputEvents.SwitchLockOnPressed();
        }
    }

    public void HandleSwitchRightHand(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.inputEvents.ChangeRightHandPressed();
        }
    }

    public void HandleSwitchConsumable(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.inputEvents.ChangeConsumablePressed();
        }
    }

    public void HandleSwitchAbility(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.inputEvents.ChangeAbilityPressed();
        }
    }

    public void HandleSwitchCharacter(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.inputEvents.ChangeCharacterPressed();
        }
    }

    //UI
    public void HandleNavigate(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            GameEventsManager.instance.UIInputEvents.NavigatePressed(context.ReadValue<Vector2>());
        }
    }

    public void HandleSubmit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.UIInputEvents.SubmitPressed();
        }
    }

    public void HandleCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.UIInputEvents.CancelPressed();
        }
    }

    public void HandlePageRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.UIInputEvents.PageRightPressed();
        }
    }

    public void HandlePageLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventsManager.instance.UIInputEvents.PageLeftPressed();
        }
    }
}
