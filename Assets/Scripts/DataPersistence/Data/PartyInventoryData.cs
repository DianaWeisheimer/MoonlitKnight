using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PartyInventoryData
{
    public List<InventorySlotData> itemData = new List<InventorySlotData>(1);

    public PartyInventoryData()
    {
        itemData = new List<InventorySlotData>(1);
    }   

    public void SaveFromInventory(PartyInventory inventory)
    {
        itemData.Clear();

        for(int i = 0; i < inventory.slots.Count; i++)
        {
            InventorySlotData newSlot = new InventorySlotData(inventory.slots[i]);
            itemData.Add(newSlot);
        }
    }
}
