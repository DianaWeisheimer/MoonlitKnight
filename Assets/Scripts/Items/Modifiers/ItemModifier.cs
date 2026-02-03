using UnityEngine;
public enum ItemModifierType { Reforge, Enchantment}

public abstract class ItemModifier : ScriptableObject
{
    public ItemModifierType modifierType;
    public abstract void Modifier(InventorySlot slot);
    /*public abstract void StatModifier();
    public abstract void OnAttackModifier();
    public abstract void OnTakeHitModifier();*/
}
