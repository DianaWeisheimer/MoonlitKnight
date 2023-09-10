 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventoryController : MonoBehaviour
{
    public static InventoryItem inventoryItem;
    public Animator animator;
    public bool opened;
    public Inventory inventory;
    public GameObject bagSlotPrefab;
    public List<GameObject> bagSlotInstantiated;
    public Transform bagSlotParent;

    public void OpenInventory()
    {
        if (opened)
        {
            animator.SetTrigger("Close");
            opened = false;
        }

        else if (!opened)
        {
            animator.SetTrigger("Open");
            opened = true;
            LoadInventory();
        }
    }

    public void LoadInventory()
    {
        ClearInventory();

        for(int i = 0; i < inventory.items.Count; i++)
        {
            GameObject newSlot = Instantiate(bagSlotPrefab, bagSlotParent);
            bagSlotInstantiated.Add(newSlot);
            if(inventory.items[i])newSlot.GetComponent<InventorySlot>().SpawnItem(inventory.items[i]);
            newSlot.GetComponent<InventorySlot>().slotID = i;
        }
    }

    public void ClearInventory()
    {
        for (int i = 0; i < bagSlotInstantiated.Count; i++)
        {
            Destroy(bagSlotInstantiated[i]);
        }

        bagSlotInstantiated.Clear();
    }

    public void RotateItem(InputAction.CallbackContext context)
    {
        //if (inventoryItem && context.performed) { inventoryItem.Rotate(); }
    }
}
