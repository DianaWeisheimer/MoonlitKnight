using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterInventory : Inventory
{
    public InventorySlot rightHand;
    public InventorySlot leftHand;
    public InventorySlot head;
    public InventorySlot body;
    public InventorySlot accessory1;
    public InventorySlot accessory2;

    public virtual void EquipItem(InventorySlot item, EquipSlots slot)
    {

    }

    public virtual void UnequipItem(InventorySlot item, EquipSlots slot)
    {

    }
}
