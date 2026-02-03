using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ReforgeModifier", menuName = "Scriptable Objects/ReforgeModifier")]
public class ReforgeModifier : ItemModifier
{
    public List<StatBonus> statBonus;

    public override void Modifier(InventorySlot slot)
    {
        throw new System.NotImplementedException();
    }

    /* public override void OnAttackModifier()
    {
        throw new System.NotImplementedException();
    }

    public override void OnTakeHitModifier()
    {
        throw new System.NotImplementedException();
    }

    public override void StatModifier()
    {
        throw new System.NotImplementedException();
    }*/
}
