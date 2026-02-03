using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public PlayerHealthBar playerHealthBar;
    //public Player player;
    //public Character character;
    public GameObject inventoryCanvas;
    public List<GameObject> menuCategories;
    //Tabs
    private int currentTab;
    public InventoryTabEquipmentUI equipmentTab;
    public GameObject firstSelectedEquipment;
    public InventoryTabItemsUI itemsTab;
    public GameObject firstSelectedInventory;
    public GameObject firstSelectedSettings;
    public EquipmentUI equipmentUI;
    //public StatListUI statListUI;
    //ItemUpdates
    public Transform inventoryUpdateContent;
    public GameObject itemUpdate;
    //Menu

    private void Update()
    {
        //playerHealthBar.SetValue(PartyManager.instance.GetMemberStats(PartyManager.instance.activeMember));
    }

    public void OpenInventory(bool open)
    {
        RefreshInventory(PartyManager.instance.inventory);

        inventoryCanvas.SetActive(open);

        CheckActiveMenu();
    }

    public void CloseInventory()
    {
        if (equipmentTab.submenu)
        {
            equipmentTab.CloseSubMenu();
        }

        else
        {
            inventoryCanvas.SetActive(false);
            InputManager.instance.ChangeActionMap("Player");
            
            //player.playerMovement.inventoryOpened = false;//
        }
    }

    private void PageRight()
    {
        currentTab++;
        currentTab = Mathf.Clamp(currentTab, 0, menuCategories.Count - 1);
        CheckActiveMenu();
    }

    private void PageLeft()
    {
        currentTab--;
        currentTab = Mathf.Clamp(currentTab, 0, menuCategories.Count - 1);
        CheckActiveMenu();
    }

    public void ChangeMenuCategory(int categoryID)
    {
        currentTab = categoryID;
        CheckActiveMenu();
        /*for(int i = 0; i< menuCategories.Count; i++)
        {
            if(i == categoryID)
            {
                menuCategories[i].SetActive(true);
            }

            else
            {
                menuCategories[i].SetActive(false);
            }
        }*/
    }

    private void CheckActiveMenu()
    {
        for (int i = 0; i < menuCategories.Count; i++)
        {
            if (i != currentTab) { menuCategories[i].SetActive(false); }
            else if (i == currentTab) { menuCategories[i].SetActive(true); }
        }

        switch (currentTab)
        {
            case 0:
                EventSystem.current.firstSelectedGameObject = firstSelectedInventory;
                break;
            case 1:
                EventSystem.current.firstSelectedGameObject = firstSelectedEquipment;
                break;
            case 3:
                EventSystem.current.firstSelectedGameObject = firstSelectedSettings;
                break;
        }
    }

    public void EquipItem(InventorySlot item, int slot)
    {
        Debug.Log("Inventory UI called equip item" + item.item);
        //character.EquipItem(item, slot);
        //RefreshInventory(character.inventory);

        //PartyManager.instance.members[PartyManager.instance.activeMember].character.EquipItem(item, slot);
        //item.equippedBy = PartyManager.instance.members[PartyManager.instance.activeMember].character;
        RefreshInventory(PartyManager.instance.inventory);
    }

    public void UnequipItem(EquipSlots slot)
    {
        //character.UnequipItem(slot);
        //PartyManager.instance.members[PartyManager.instance.activeMember].character.UnequipItem(slot);
        RefreshInventory(PartyManager.instance.inventory);
    }

    public void RefreshInventory(PartyInventory characterInventory)
    {
        equipmentTab.RefreshEquips();
        itemsTab.RefreshItems();
        equipmentUI.RefreshImages(characterInventory);
        //statListUI.RefreshStatList();
    }

    //Settings

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SaveGame()
    {
        DataPersistenceManager.instance.SaveGame();
    }

    //Item Update
    public void SpawnItemUpdate(CharacterInventory characterInventory ,InventoryItem item)
    {
        GameObject instantiatedUpdate = Instantiate(itemUpdate, inventoryUpdateContent);
        instantiatedUpdate.transform.SetParent(inventoryUpdateContent);
        ItemUpdateUI itemUpdateUI = instantiatedUpdate.GetComponent<ItemUpdateUI>();
        itemUpdateUI.SetButton(item);
    }

    private void OnEnable()
    {
        GameEventsManager.instance.UIInputEvents.onCancelPressed += CloseInventory;
        GameEventsManager.instance.UIInputEvents.onPageRightPressed += PageRight;
        GameEventsManager.instance.UIInputEvents.onPageLeftPressed += PageLeft;

        //player.character.stats.Died += CloseInventory;
        //CharacterInventory.AddedItem += SpawnItemUpdate;
        //CharacterInventory.UpdatedInventory += RefreshInventory;
        PlayerMovement.OpenInventory += OpenInventory;
        //InventoryItemButtonUI.EquipItem += EquipItem;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.UIInputEvents.onCancelPressed -= CloseInventory;
        GameEventsManager.instance.UIInputEvents.onPageRightPressed -= PageRight;
        GameEventsManager.instance.UIInputEvents.onPageLeftPressed -= PageLeft;

        //player.character.stats.Died -= CloseInventory;
        //haracterInventory.AddedItem -= SpawnItemUpdate;
        //CharacterInventory.UpdatedInventory -= RefreshInventory;
        PlayerMovement.OpenInventory -= OpenInventory;
        //InventoryItemButtonUI.EquipItem -= EquipItem;
    }
}
