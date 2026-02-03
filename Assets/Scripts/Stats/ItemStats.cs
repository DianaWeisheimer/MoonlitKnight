using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStats
{
    public InventoryItem inventoryItem;
    public Stat bonusHealth;
    public Stat bonusMana;
    public Stat bonusStamina;
    public Stat bonusVitality;
    public Stat bonusWisdom;
    public Stat bonusEndurance;
    public Stat bonusDefense;
    public Stat bonusStrength;
    public Stat bonusDexterity;
    public Stat bonusFaith;
    public Stat bonusDarkness;
    public Stat minDamage;
    public Stat maxDamage;

    public void CalculateStats()
    {
        //Base Stats
        if(inventoryItem.item is WeaponItem)
        {
            WeaponItem weaponItem = inventoryItem.item as WeaponItem;
            //bonusHealth.SetBaseValue(weaponItem.baseStat.baseHealth, 0);
            //minDamage.SetBaseValue(weaponItem.baseStat.baseMinDamage, 0);
            //maxDamage.SetBaseValue(weaponItem.baseStat.baseMaxDamage, 0);
        }

        else if (inventoryItem.item is ArmorItem)
        {
            ArmorItem armorItem = inventoryItem.item as ArmorItem;
            bonusHealth.SetBaseValue(armorItem.baseStat.baseHealth);
        }

        //Modifiers
    }
}
