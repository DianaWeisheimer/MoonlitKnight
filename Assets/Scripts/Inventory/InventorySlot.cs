using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public InventoryItem item;
    public int slotID;
    public GameObject inventoryItemPrefab;
    public GameObject inventoryItemInstantiated;

    public void SpawnItem(Item itemToSpawn)
    {
        GameObject inventoryItemInstantiated = Instantiate(inventoryItemPrefab, transform);
        inventoryItemInstantiated.GetComponent<InventoryItem>().LoadItem(itemToSpawn);
        inventoryItemInstantiated.GetComponent<InventoryItem>().currentSlot = this;
    }

    public void AddItem(InventoryItem newItem)
    {
        item = newItem;
    }

    public void RemoveItem()
    {
        item = null;
    }

}
