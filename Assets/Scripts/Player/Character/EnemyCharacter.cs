using UnityEngine;

public class EnemyCharacter : Character
{
    private void Start()
    {
        StartCoroutine(abilityHolder.UpdateAbilityUIRoutine());
        inventory = GetComponent<CharacterInventory>();

    }
    private void FixedUpdate()
    {
        stats.Recharge();
        abilityHolder.Update();
    }

    public override void EquipItem(InventorySlot item, EquipSlots slot)
    {
        if(item.item is EquipmentItem)
        {
            equipment.EquipItem(item, slot);

            switch(slot)
            {
                case EquipSlots.RightHand:
                    inventory.rightHand = item;
                    break;
                case EquipSlots.LeftHand:
                    inventory.leftHand = item;
                    break;
                case EquipSlots.Head:
                    inventory.head = item;
                    break;
                case EquipSlots.Body:
                    inventory.body = item;
                    break;
                case EquipSlots.Accessory1:
                    inventory.accessory1 = item;
                    break;
                case EquipSlots.Accessory2:
                    inventory.accessory2 = item;
                    break;
            }

            stats.CalculateItemBonuses(this);
        }
    }

    public override void UnequipItem(EquipSlots slot)
    {
        InventorySlot itemToUnequip = null;
        equipment.UnequipItem(slot);

        switch (slot)
        {
            case EquipSlots.RightHand:
                itemToUnequip = inventory.rightHand;
                inventory.rightHand = new InventorySlot(null, 0, null);
                break;
            case EquipSlots.LeftHand:
                itemToUnequip = inventory.leftHand;
                inventory.leftHand = new InventorySlot(null, 0, null);
                break;
            case EquipSlots.Head:
                itemToUnequip = inventory.head;
                inventory.head = new InventorySlot(null, 0, null);
                break;
            case EquipSlots.Body:
                itemToUnequip = inventory.body;
                inventory.body = new InventorySlot(null, 0, null);
                break;
            case EquipSlots.Accessory1:
                itemToUnequip = inventory.accessory1;
                inventory.accessory1 = new InventorySlot(null, 0, null);
                break;
            case EquipSlots.Accessory2:
                itemToUnequip = inventory.accessory2;
                inventory.accessory1 = new InventorySlot(null, 0, null);
                break;
        }

        stats.CalculateItemBonuses(this);
    }
}
