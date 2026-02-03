using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootDrop
{
    public float dropChance;
    public InventorySlot item;
    //public int ammount;

    public bool CheckDropChance()
    {
        float drop = Random.Range(0, 100);
        
        if(drop <= dropChance) { return true; }
        else { return false; }
    }
}
