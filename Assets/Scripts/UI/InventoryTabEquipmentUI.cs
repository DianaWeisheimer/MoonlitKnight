using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTabEquipmentUI : MonoBehaviour
{
    //public InventoryUI inventoryUI;
    public List<EquipItemButtonUI> equipItemButtons;
    public GameObject itemsSubMenu;
    public bool submenu;
    public GameObject itemButton;
    public Transform equipInventoryContent;
    public List<GameObject> spawnedEquipItemButton;
    public void RefreshEquips()
    {
        //for(int i = 0; i<equipItemButtons.Count; i++)
        //{
            //equipItemButtons[i].UpdateImage(inventoryUI.character.inventory);
            //equipItemButtons[i].UpdateImage(PartyManager.instance.members[PartyManager.instance.activeMember].character.inventory);
        //}
    }

    public void LoadEquipItem(EquipItemButtonUI equipItemButton)
    {
        itemsSubMenu.SetActive(true);
        submenu = true;

        ClearEquipItem();

        //for (int i = 0; i < inventoryUI.character.inventory.items.Count; i++)
        for (int i = 0; i < PartyManager.instance.inventory.slots.Count; i++)
        {
            //if (inventoryUI.character.inventory.items[i].item.itemType == equipItemButton.itemType)
            if (PartyManager.instance.inventory.slots[i].item.itemType == equipItemButton.itemType)
            {
                GameObject button = Instantiate(itemButton, equipInventoryContent);
                InventoryItemButtonUI inventoryItemButtonUI = button.GetComponent<InventoryItemButtonUI>();

                //inventoryItemButtonUI.SetButton(inventoryUI.character.inventory.items[i]);
                inventoryItemButtonUI.SetButton(PartyManager.instance.inventory.slots[i], true);

                inventoryItemButtonUI.equip = true;
                //inventoryItemButtonUI.equipSlot = (int)equipItemButton.equipSlot;
                spawnedEquipItemButton.Add(button);
            }
        }
    }

    public void ClearEquipItem()
    {
        if (spawnedEquipItemButton.Count != 0)
        {
            for (int i = 0; i < spawnedEquipItemButton.Count; i++)
            {
                GameObject buttonToDestroy = spawnedEquipItemButton[i];
                Destroy(buttonToDestroy);
            }

            spawnedEquipItemButton.Clear();
        }     
    }

    public void CloseSubMenu()
    {
        //itemsSubMenu.SetActive(false);
        submenu = false;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.playerMenuEvents.onCloseEquipmentSubMenu += CloseSubMenu;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerMenuEvents.onCloseEquipmentSubMenu -= CloseSubMenu;
    }
}
