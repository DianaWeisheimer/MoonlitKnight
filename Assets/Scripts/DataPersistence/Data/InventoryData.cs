using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public List<InventorySlotData> itemData = new List<InventorySlotData>(1);

    public InventoryData()
    {
        itemData = new List<InventorySlotData>(1);
    }   

    public void SaveFromInventory(Inventory inventory)
    {
        itemData.Clear();

        for(int i = 0; i < inventory.slots.Count; i++)
        {
            InventorySlotData newSlot = new InventorySlotData(inventory.slots[i]);
            itemData.Add(newSlot);
        }
    }

    public void AddItem(InventoryItem item)
    {
        /*InventorySlotData itemToAdd = new InventorySlotData();
        itemToAdd.SaveItemData(item);
        itemData.Add(itemToAdd);
        //Debug.Log("Added item to InventoryData");*/
    }

    public void AddEquippedItems(InventoryItem item, int slot)
    {
        /*InventorySlotData itemToAdd = new InventorySlotData();
        itemToAdd.SaveItemData(item);
        equippedItems[slot] = itemToAdd;*/
    }
}
