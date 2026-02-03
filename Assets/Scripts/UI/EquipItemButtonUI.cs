using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum EquipSlots
{
    RightHand, LeftHand, Head, Body, Accessory1, Accessory2, Consumable1, Consumable2, Consumable3
}

public class EquipItemButtonUI : MonoBehaviour, IPointerClickHandler
{
    public static EquipItemButtonUI activeButton;
    public EquipSlots equipSlot;
    public ItemCategory itemType;
    public Image image;
    public InventoryUI inventoryUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventoryUI.UnequipItem(equipSlot);
        }
            
    }

    public void SelectButton()
    {
        activeButton = this;
    }

    public void UpdateImage(CharacterInventory inventory)
    {
        /*if(inventory.equippedItems[(int)equipSlot].item != null)
        {
            image.color = new Color32(255, 255, 255, 255);
            image.sprite = inventory.equippedItems[(int)equipSlot].item.itemImage;
        }

        else
        {
            image.color = new Color32(255, 255, 255, 0);
        }*/
    }
}
