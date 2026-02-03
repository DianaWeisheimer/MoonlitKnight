using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();
    public virtual void AddItem(InventorySlot slot)
    {
        Item item = slot.item;
        //Check if item is in inventory
        if (item.stackable)
        {
            //Look for item with same name
            for(int i = 0; i < slots.Count; i++)
            {
                //If item has equal name
                if(slots[i].item.itemName == item.itemName)
                {
                    //Add item to stack
                    slots[i].stackAmmount++;
                    slots[i].stackAmmount = Mathf.Clamp(slots[i].stackAmmount, 1, 99);
                    return;
                }
            }
        }

        else
        {
            InventorySlot newSlot = new InventorySlot(item, 1, null);
            slots.Add(newSlot);
        }
    } 

    public virtual void RemoveItem(int index)
    {
        slots.RemoveAt(index);
    }
    
    public virtual InventorySlot GetItem(string itemName, ItemCategory itemType)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item.itemName == itemName && slots[i].item.itemType == itemType)
            {
                return slots[i];
            }
        }

        return null;
    }

    public List<InventorySlot> GetWeaponsByCategory(WeaponCategory category)
    {
        List<InventorySlot> itemsToReturn = new List<InventorySlot>();

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item.itemType == ItemCategory.Weapon)
            {
                WeaponItem weapon = slots[i].item as WeaponItem;
                if (weapon.category == category)
                {
                    itemsToReturn.Add(slots[i]);
                }
            }
        }

        return itemsToReturn;
    }

    public void LoadFromInventoryData(InventoryData data)
    {
        slots.Clear();

        for(int i = 0; i < data.itemData.Count; ++i)
        {
            InventorySlot newSlot = new InventorySlot(null, 1, null);
            newSlot.LoadFromSlotData(data.itemData[i]);

            slots.Add(newSlot);
        }
    }
}
