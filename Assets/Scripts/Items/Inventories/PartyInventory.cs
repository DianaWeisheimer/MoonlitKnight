using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyInventory : Inventory
{
    [SerializeField] private List<InventorySlot> consumables = new List<InventorySlot>(2);
    [SerializeField] private int activeConsumable = 0;

    public void EquipConsumable(InventorySlot item, int i)
    {
        consumables[i] = item;
    }

    public void UnequipConsumable(int i)
    {
        consumables[i] = null;
    }

    public void UseConsumable(Character chatracter, int i)
    {
        if (consumables[i] == null || consumables[i].item == null) return;

        ConsumableItem item = consumables[i].item as ConsumableItem;

        if (item.refillable && consumables[i].GetItemCharges() >= 1) 
        {
            item.effects[0].Consume(chatracter);
            consumables[i].ChargeItem(-1); 
        }

        else if (!item.refillable && consumables[i].stackAmmount >= 1)
        {
            item.effects[0].Consume(chatracter);
            consumables[i].stackAmmount--;
            if (consumables[i].stackAmmount <= 0) slots.Remove(consumables[i]);

            for (int j = 0; j < consumables.Count; j++)
            {
                if (consumables[j].stackAmmount <= 0)
                {
                    consumables[j] = new InventorySlot(null, 1, null);
                }
            }
        }
    }

    public InventorySlot GetConsumable(int i)
    {
        return consumables[i];
    }

    public void LoadFromPartyInventoryData(PartyInventoryData data)
    {
        slots.Clear();

        for (int i = 0; i < data.itemData.Count; ++i)
        {
            InventorySlot newSlot = new InventorySlot(null, 1, null);
            newSlot.LoadFromSlotData(data.itemData[i]);

            slots.Add(newSlot);
        }
    }
}
