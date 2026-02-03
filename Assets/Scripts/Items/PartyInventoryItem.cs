using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PartyInventoryItem : InventoryItem
{
    public bool equipped;

    public PartyInventoryItem(InventoryItem newItem):base(newItem)
    {

    }
}
