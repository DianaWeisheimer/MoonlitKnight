using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class InventorySlot
{
    public Item item;
    [Min(1)]public int stackAmmount = 1;
    public List<ItemModifier> modifier;
    //Equipping
    public bool isEquipped;
    public Character equippedBy;
    [SerializeField]private int charges;

    public InventorySlot(Item newIitem, int stack, List<ItemModifier> modifiers)
    {
        item = newIitem;
        stackAmmount = stack;
        modifier = modifiers;
    }

    public void LoadFromSlotData(InventorySlotData slotData)
    {
        item = DataPersistenceManager.instance.database.items[slotData.itemID];
        stackAmmount = slotData.stackAmmount;
        isEquipped = slotData.equipped;
    }

    public ItemStatBonus GetStatBonuses()
    {
        ItemStatBonus statBonus = new ItemStatBonus();

        if(item is WeaponItem weaponItem)
        {
            statBonus = weaponItem.statBonus;
        }
        else if(item is ArmorItem armorItem)
        {
            statBonus = armorItem.statBonus;
        }

        //Implement Enchantments and Reforges

        return statBonus;
    }

    public void ChargeItem(int i)
    {
        charges += i;
    }

    public int GetItemCharges()
    {
        return charges;
    }
}
