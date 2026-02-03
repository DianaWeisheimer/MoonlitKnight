using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemButtonUI : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemAmmount;
    public Image itemImage;
    /*public static event Action<InventorySlot, EquipSlots, int> EquipItem;
    public static event Action Clicked;
    public static event Action<InventorySlot> OpenDetails;*/
    public InventorySlot slot;
    public bool equip;
    public int partyMemberIndex;
    public EquipSlots equipSlot;

    public void SetButton(InventorySlot inventoryItem, bool active)
    {
        slot = inventoryItem;
        itemName.text = inventoryItem.item.itemName;
        //itemDescription.text = inventoryItem.item.itemDescription;

        if (inventoryItem.item.itemImage != null)
        {
            itemImage.sprite = inventoryItem.item.itemImage;
        }

        if (inventoryItem.item.stackable)
        {
            itemAmmount.text = inventoryItem.stackAmmount.ToString();
        }

        if(inventoryItem.item is ConsumableItem)
        {
            ConsumableItem consumable = inventoryItem.item as ConsumableItem;
            if(consumable.refillable)
            {
                itemAmmount.text = inventoryItem.GetItemCharges().ToString();
            }
        }

        else
        {
            itemAmmount.gameObject.SetActive(false);
        }

        if(active == false)
        {
            GetComponent<Button>().interactable = false;
            itemName.color = Color.gray;
            itemAmmount.color = Color.gray;
        }
    }

    public void OnClick()
    {
        if (equip)
        {
            GameEventsManager.instance.playerMenuEvents.EquipItemPressed(slot, equipSlot, partyMemberIndex);
        }

        else
        {
            GameEventsManager.instance.playerMenuEvents.OpenItemDetails(slot);
        }
    }
}
