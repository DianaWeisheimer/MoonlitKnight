using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PartyMemberInventory : CharacterInventory
{
    public override void AddItem(InventorySlot item)
    {
        PartyManager.instance.inventory.AddItem(item);
    }
}
