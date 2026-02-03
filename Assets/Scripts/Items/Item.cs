using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCategory { Consumable, Key, Material, Weapon, Armor, Accessory }
public enum WeaponCategory { Sword, Axe, Bow, Gun, Dagger, Polearm, Greatsword, Staff, Shield, Ammo }
public enum ArmorCategory { Plate, Mail, Leather, Cloth }

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/New Item")]
public class Item : ScriptableObject
{
    public int itemID;
    public ItemCategory itemType;
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int itemRarity;
    public bool stackable;
    public int stackLimit;
}
