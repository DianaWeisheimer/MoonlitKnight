using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlotData
{
    public int itemID;
    public int stackAmmount;
    public List<int> modifiers;
    public bool equipped;

    public InventorySlotData(InventorySlot item)
    {
        itemID = item.item.itemID;
        stackAmmount = item.stackAmmount;
        equipped = item.isEquipped;
    }
}
