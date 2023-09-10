using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    [SerializeField] InventoryGrid selectedItemGrid;
    public PlayerInput playerInput;

    private void Start()
    {
        if (!playerInput) { playerInput = FindObjectOfType<PlayerInput>(); }
    }

    void Update()
    {
        if(selectedItemGrid == null) { return; }
        Debug.Log(selectedItemGrid.GetTilePosition(playerInput.actions["Mouse"].ReadValue<Vector2>()));
    }
}
