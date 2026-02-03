using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class EquipmentButton : MonoBehaviour, IPointerClickHandler
{
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI slotName;
    public Sprite emptyItemSprite;
    public EquipSlots equipSlots;
    public ItemCategory itemCategory;
    public Button button;
    public Color32[] textColors;
    public int memberIndex;

    public void RefreshInformation(InventorySlot item, Job partyMemberJob, int index)
    {
        memberIndex = index;
        switch(equipSlots)
        {
            case EquipSlots.RightHand:
                if(partyMemberJob.rightHandType.Count == 0) SetButtonActive(false);
                else SetButtonActive(true);
                break;
            case EquipSlots.LeftHand:
                if (partyMemberJob.leftHandType.Count == 0) SetButtonActive(false);
                else SetButtonActive(true);
                break;
            case EquipSlots.Head:
                if (partyMemberJob.armorTypes.Count == 0) SetButtonActive(false);
                else SetButtonActive(true);
                break;
            case EquipSlots.Body:
                if (partyMemberJob.armorTypes.Count == 0) SetButtonActive(false);
                else SetButtonActive(true);
                break;
        }

        if (item.item == null)
        {
            itemName.text = "";
            itemImage.sprite = emptyItemSprite;
            return;
        }


        if(item.item.itemImage != null)
        {
            itemImage.sprite = item.item.itemImage;
        }

        else
        {
            itemImage.sprite = emptyItemSprite;
        }

        if (item.item is ConsumableItem)
        {
            ConsumableItem consumableItem = item.item as ConsumableItem;
            if (consumableItem.refillable)
            {
                itemName.text = item.GetItemCharges().ToString();
            }

            else
            {
                itemName.text = item.stackAmmount.ToString();
            }
        }

        else
        {
            itemName.text = item.item.itemName;
        }

    }

    public void SetButtonActive(bool hehe)
    {
        if(hehe)
        {
            button.interactable = true;
            itemName.color = textColors[0];
            slotName.color = textColors[0];
        }

        else
        {
            button.interactable = false;
            itemName.color = textColors[1];
            slotName.color = textColors[1];
        }
    }

    public void PressedButton()
    {
        GameEventsManager.instance.playerMenuEvents.EquipmentButtonPressed(equipSlots, itemCategory);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            GameEventsManager.instance.playerMenuEvents.UnequipItemPressed(equipSlots, memberIndex);
        }
    }
}
