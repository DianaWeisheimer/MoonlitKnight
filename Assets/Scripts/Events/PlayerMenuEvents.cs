using System;
using UnityEngine;

public class PlayerMenuEvents
{
    public event Action<EquipSlots, ItemCategory> onEquipmentButtonPressed;
    public void EquipmentButtonPressed(EquipSlots slot, ItemCategory category)
    {
        onEquipmentButtonPressed?.Invoke(slot, category);
    }

    public event Action<InventorySlot> onOpenItemDetails;
    public void OpenItemDetails(InventorySlot slot)
    {
        onOpenItemDetails?.Invoke(slot);
    }

    public event Action<InventorySlot, EquipSlots, int> onEquipItemPressed;
    public event Action onCloseEquipmentSubMenu;
    public void EquipItemPressed(InventorySlot inventorySlot, EquipSlots equipSlots, int index)
    {
        onEquipItemPressed?.Invoke(inventorySlot, equipSlots, index);
        onCloseEquipmentSubMenu?.Invoke();
    }

    public event Action<EquipSlots, int> onUnequipItemPressed;
    internal void UnequipItemPressed(EquipSlots equipSlots, int memberIndex)
    {
        onUnequipItemPressed?.Invoke(equipSlots, memberIndex);
    }
}
