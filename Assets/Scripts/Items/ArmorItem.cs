using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArmor", menuName = "Item/New Armor")]
public class ArmorItem : EquipmentItem
{
    public BaseStat baseStat;
    public ArmorObject armorObject;
    public ArmorCategory category;
}
