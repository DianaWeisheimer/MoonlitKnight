using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewConsumable", menuName = "Item/New Consumable")]
public class ConsumableItem : Item
{
    public bool potionIngredient;
    public bool potionBase;
    public bool refillable;
    public int maxRefillAmmount;
    public ConsumableObject consumableObject;
    public string animationToPlay;
    public List<ConsumableEffect> effects;
}
