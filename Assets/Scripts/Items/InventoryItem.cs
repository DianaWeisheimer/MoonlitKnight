using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public Item item;
    public List<ItemEnchantment> enchantments;
    public ItemReforge reforge;
    public int stackAmmount = 1;
    public Stat bonusHealth;
    public Stat bonusMana;
    public Stat bonusStamina;

    public Stat bonusStrength;
    public Stat bonusDexterity;
    public Stat bonusIntelligence;
    public Stat bonusWisdom;

    public Stat bonusPhysicalDefense;
    public Stat bonusMagicDefense;

    public Stat scalingStrength;
    public Stat scalingDexterity;
    public Stat scalingIntelligence;
    public Stat scalingWisdom;

    public Stat minDamage;
    public Stat maxDamage;
    public Stat magicDamage;

    public InventoryItem(InventoryItem newItem)
    {
        item = newItem.item;
        enchantments = newItem.enchantments;
        reforge = newItem.reforge;
        stackAmmount = newItem.stackAmmount;
        bonusHealth = newItem.bonusHealth;
        bonusMana = newItem.bonusMana;
        bonusStamina = newItem.bonusStamina;
        bonusStrength = newItem.bonusStrength;
        bonusDexterity = newItem.bonusDexterity;
        bonusIntelligence = newItem.bonusIntelligence;
        bonusWisdom = newItem.bonusWisdom;
        bonusPhysicalDefense = newItem.bonusPhysicalDefense;
        bonusMagicDefense = newItem.bonusMagicDefense;
        scalingStrength = newItem.scalingStrength;
        scalingDexterity = newItem.scalingDexterity;
        scalingIntelligence = newItem.scalingIntelligence;
        scalingWisdom = newItem.scalingWisdom;
        minDamage = newItem.minDamage;
        maxDamage = newItem.maxDamage;
        magicDamage = newItem.magicDamage;
    }

    /*public void CalculateStats()
    {
        if(item is WeaponItem)
        {
            //Base Stat
            WeaponItem weaponItem = item as WeaponItem;
            bonusHealth.SetBaseValue(weaponItem.baseStat.baseHealth, 0);
            bonusMana.SetBaseValue(weaponItem.baseStat.baseMana, 0);
            bonusStamina.SetBaseValue(weaponItem.baseStat.baseStamina, 0);
            bonusStrength.SetBaseValue(weaponItem.baseStat.baseStrength, 0);
            bonusDexterity.SetBaseValue(weaponItem.baseStat.baseDexterity, 0);
            bonusIntelligence.SetBaseValue(weaponItem.baseStat.baseIntelligence, 0);
            bonusWisdom.SetBaseValue(weaponItem.baseStat.baseWisdom, 0);
            bonusPhysicalDefense.SetBaseValue(weaponItem.baseStat.basePhysicalDefense, 0);
            bonusMagicDefense.SetBaseValue(weaponItem.baseStat.baseMagicDefense, 0);

            minDamage.SetBaseValue(weaponItem.baseStat.baseMinDamage, 0);
            maxDamage.SetBaseValue(weaponItem.baseStat.baseMaxDamage, 0);
            magicDamage.SetBaseValue(weaponItem.baseStat.magicDamage, 0);

            //Reseting Modifiers
            bonusHealth.SetModifier(0, 0);
            bonusMana.SetModifier(0, 0);
            bonusStamina.SetModifier(0, 0);
            bonusStrength.SetModifier(0, 0);
            bonusDexterity.SetModifier(0, 0);
            bonusIntelligence.SetModifier(0, 0);
            bonusWisdom.SetModifier(0, 0);
            bonusPhysicalDefense.SetModifier(0, 0);
            bonusMagicDefense.SetModifier(0, 0);

            minDamage.SetModifier(0, 0);
            maxDamage.SetModifier(0, 0);
            magicDamage.SetModifier(0, 0);

            //Enchantments
            if(enchantments != null)
            {
                for(int i = 0; i < enchantments.Count; i++)
                {
                    bonusHealth.AddModifier(enchantments[i].healthBonus, enchantments[i].healthMultiplier);
                    bonusMana.AddModifier(enchantments[i].manaBonus, enchantments[i].manaMultiplier);
                    bonusStamina.AddModifier(enchantments[i].staminaBonus, enchantments[i].staminaMultiplier);
                    bonusStrength.AddModifier(enchantments[i].strengthBonus, enchantments[i].strengthBonus);
                    bonusDexterity.AddModifier(enchantments[i].dexterityBonus, enchantments[i].dexterityMultiplier);
                    bonusIntelligence.AddModifier(enchantments[i].intelligenceBonus, enchantments[i].intelligenceMultiplier);
                    bonusWisdom.AddModifier(enchantments[i].wisdomBonus, enchantments[i].wisdomMultiplier);
                    bonusPhysicalDefense.AddModifier(enchantments[i].physicalDefenseBonus, enchantments[i].physicalDefenseMultiplier);
                    bonusMagicDefense.AddModifier(enchantments[i].magicDefenseBonus, enchantments[i].magicDefenseMultiplier);

                    minDamage.AddModifier(enchantments[i].damageBonus, enchantments[i].damageMultiplier);
                    maxDamage.AddModifier(enchantments[i].damageBonus, enchantments[i].damageMultiplier);
                    magicDamage.AddModifier(enchantments[i].magicDamageBonus, enchantments[i].magicDamageMultiplier);
                }
            }

            //Reforge
            if(reforge != null)
            {
                bonusHealth.AddModifier(reforge.healthBonus, reforge.healthMultiplier);
                bonusMana.AddModifier(reforge.manaBonus, reforge.manaMultiplier);
                bonusStamina.AddModifier(reforge.staminaBonus, reforge.staminaMultiplier);
                bonusStrength.AddModifier(reforge.strengthBonus, reforge.strengthBonus);
                bonusDexterity.AddModifier(reforge.dexterityBonus, reforge.dexterityMultiplier);
                bonusIntelligence.AddModifier(reforge.intelligenceBonus, reforge.intelligenceMultiplier);
                bonusWisdom.AddModifier(reforge.wisdomBonus, reforge.wisdomMultiplier);
                bonusPhysicalDefense.AddModifier(reforge.physicalDefenseBonus, reforge.physicalDefenseMultiplier);
                bonusMagicDefense.AddModifier(reforge.magicDefenseBonus, reforge.magicDefenseMultiplier);

                minDamage.AddModifier(reforge.damageBonus, reforge.damageMultiplier);
                maxDamage.AddModifier(reforge.damageBonus, reforge.damageMultiplier);
                magicDamage.AddModifier(reforge.magicDamageBonus, reforge.magicDamageMultiplier);
            }
        }
    }*/

    public void LoadItemData(InventorySlotData data)
    {
        /*if(data.itemType == 0) { item = ItemManagerSingleton.Instance.consumableItems[data.itemID];
            Debug.Log("Load item data" + ItemManagerSingleton.Instance.consumableItems[data.itemID].itemName);
        }

        if(data.itemType == 3) { item = ItemManagerSingleton.Instance.weaponItems[data.itemID];
            Debug.Log("Load item data" + ItemManagerSingleton.Instance.weaponItems[data.itemID].itemName);
        }

        if(data.itemType == 4) { item = ItemManagerSingleton.Instance.armorItems[data.itemID];
            Debug.Log("Load item data" + ItemManagerSingleton.Instance.armorItems[data.itemID].itemName);
        }

        stackAmmount = data.stackAmmount;*/
    }
}
