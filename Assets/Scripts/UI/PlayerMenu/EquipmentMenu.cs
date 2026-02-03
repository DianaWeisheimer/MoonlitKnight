using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentMenu : PlayerMenuPage
{
    public List<EquipmentButton> equipmentButtons;

    public Transform partyMemberCardParent;
    public GameObject partyMemberCard1;
    public PartyMemberCard partyMemberCard2;
    public List<GameObject> spawnedPartyMemberCards;

    public GameObject itemSelectionSubMenu;
    private bool subMenu;
    public GameObject itemButton;
    public List<GameObject> spawnedItemButton;
    public GameObject inventoryContent;

    public int partyMemberIndex;

    public CharacterStatsPanel statsPanel;

    public override void OnPageOpen()
    {
        RefreshInformation();
    }

    public override bool CloseSubMenu()
    {
        if(subMenu)
        {
            itemSelectionSubMenu.SetActive(false);
            RefreshInformation();
            subMenu = false;    
            return true;
        }

        else
        {
            return false;
        }
    }

    public void SelectPartyMember(int index)
    {
        partyMemberIndex = index;
        RefreshInformation();
    }

    public void RefreshInformation()
    {
        PlayerCharacter member = PartyManager.instance.GetMember(partyMemberIndex).core.character;

        partyMemberCard2.SetCard(member);
        ClearPartyMemberCards();

        equipmentButtons[0].RefreshInformation(member.inventory.rightHand, member.job, partyMemberIndex);
        equipmentButtons[1].RefreshInformation(member.inventory.leftHand, member.job, partyMemberIndex);
        equipmentButtons[2].RefreshInformation(member.inventory.head, member.job, partyMemberIndex);
        equipmentButtons[3].RefreshInformation(member.inventory.body, member.job, partyMemberIndex);
        equipmentButtons[4].RefreshInformation(member.inventory.accessory1, member.job, partyMemberIndex);
        equipmentButtons[5].RefreshInformation(member.inventory.accessory2, member.job, partyMemberIndex);
        equipmentButtons[6].RefreshInformation(PartyManager.instance.inventory.GetConsumable(0), member.job, partyMemberIndex);
        equipmentButtons[7].RefreshInformation(PartyManager.instance.inventory.GetConsumable(1), member.job, partyMemberIndex);
        equipmentButtons[8].RefreshInformation(PartyManager.instance.inventory.GetConsumable(2), member.job, partyMemberIndex);

        /*for (int i = 0; i < equipmentButtons.Count; i++)
        {
            equipmentButtons[i].RefreshInformation(member.inventory.equipmentSlots[i], member.job, partyMemberIndex);
        }*/

        for (int i = 0; i < PartyManager.instance.members.Count; i++)
        {
            GameObject card = Instantiate(partyMemberCard1);
            card.transform.SetParent(partyMemberCardParent);
            card.transform.localScale = Vector3.one;
            card.GetComponent<PartyMemberCard>().SetCard(PartyManager.instance.GetMember(i).core.character);
            card.GetComponent<PartyMemberCard>().partyMemberIndex = i;
            card.GetComponent<PartyMemberCard>().equipmentMenu = true;
            spawnedPartyMemberCards.Add(card);
        }

        statsPanel.RefreshInformation(member.stats);
    }

    public void ClearPartyMemberCards()
    {
        if (spawnedPartyMemberCards.Count != 0)
        {
            for (int i = 0; i < spawnedPartyMemberCards.Count; i++)
            {
                GameObject buttonToDestroy = spawnedPartyMemberCards[i];
                Destroy(buttonToDestroy);
            }

            spawnedPartyMemberCards.Clear();
        }
    }

    public void OpenItemSelectionSubMenu(EquipSlots slot, ItemCategory category)
    {
        ClearItemCategory();

        subMenu = true;

        itemSelectionSubMenu.SetActive(true);

        for (int i = 0; i < PartyManager.instance.inventory.slots.Count; i++)
        {
            InventorySlot itemSlot = PartyManager.instance.inventory.slots[i];

            if (itemSlot.item == null)
            {
                return;
            }

            switch (slot)
            {
                case EquipSlots.RightHand:
                    if (itemSlot.item.itemType == ItemCategory.Weapon)
                    {
                        WeaponItem rightHandItem = (WeaponItem)itemSlot.item;

                        for (int j = 0; j < PartyManager.instance.GetMemberJob(partyMemberIndex).rightHandType.Count; j++)
                        {
                            if (rightHandItem.category == PartyManager.instance.GetMemberJob(partyMemberIndex).rightHandType[j])
                            {
                                GameObject buttonRight = Instantiate(itemButton, inventoryContent.transform);
                                buttonRight.GetComponent<InventoryItemButtonUI>().SetButton(itemSlot, true);
                                buttonRight.GetComponent<InventoryItemButtonUI>().equip = true;
                                buttonRight.GetComponent<InventoryItemButtonUI>().partyMemberIndex = partyMemberIndex;
                                spawnedItemButton.Add(buttonRight);
                            }
                        }
                    }

                break;

                case EquipSlots.LeftHand:
                    if (itemSlot.item.itemType == ItemCategory.Weapon)
                    {
                        WeaponItem leftHandItem = (WeaponItem)itemSlot.item;

                        for (int j = 0; j < PartyManager.instance.GetMemberJob(partyMemberIndex).leftHandType.Count; j++)
                        {
                            if (leftHandItem.category == PartyManager.instance.GetMemberJob(partyMemberIndex).leftHandType[j])
                            {
                                GameObject buttonLeft = Instantiate(itemButton, inventoryContent.transform);
                                buttonLeft.GetComponent<InventoryItemButtonUI>().SetButton(itemSlot, !itemSlot.isEquipped);
                                buttonLeft.GetComponent<InventoryItemButtonUI>().equip = true;
                                buttonLeft.GetComponent<InventoryItemButtonUI>().equipSlot = EquipSlots.LeftHand;
                                buttonLeft.GetComponent<InventoryItemButtonUI>().partyMemberIndex = partyMemberIndex;
                                spawnedItemButton.Add(buttonLeft);
                            }
                        }
                    }

                break;

                default:
                    if(itemSlot.item.itemType == category)
                    {
                        GameObject button = Instantiate(itemButton, inventoryContent.transform);
                        button.GetComponent<InventoryItemButtonUI>().SetButton(itemSlot, !itemSlot.isEquipped);
                        button.GetComponent<InventoryItemButtonUI>().equip = true;
                        button.GetComponent<InventoryItemButtonUI>().equipSlot = slot;
                        spawnedItemButton.Add(button);
                    }
                break;
            }
        }
    }

    public void ClearItemCategory()
    {
        if (spawnedItemButton.Count != 0)
        {
            for (int i = 0; i < spawnedItemButton.Count; i++)
            {
                GameObject buttonToDestroy = spawnedItemButton[i];
                Destroy(buttonToDestroy);
            }

            spawnedItemButton.Clear();
        }
    }

    public void CloseItemSelectionSubMenu()
    {
        subMenu = false;
        ClearItemCategory();
        itemSelectionSubMenu.SetActive(false);
        RefreshInformation();
    }

    public void UnequipItem(EquipSlots slot, int hehe)
    {
        RefreshInformation();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.partyEvents.onPartyUnequipItem += UnequipItem;
        GameEventsManager.instance.playerMenuEvents.onEquipmentButtonPressed += OpenItemSelectionSubMenu;
        GameEventsManager.instance.playerMenuEvents.onCloseEquipmentSubMenu += CloseItemSelectionSubMenu;
        PartyMemberCard.OnClick += SelectPartyMember;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.partyEvents.onPartyUnequipItem -= UnequipItem;
        GameEventsManager.instance.playerMenuEvents.onEquipmentButtonPressed -= OpenItemSelectionSubMenu;
        GameEventsManager.instance.playerMenuEvents.onCloseEquipmentSubMenu -= CloseItemSelectionSubMenu;
        PartyMemberCard.OnClick -= SelectPartyMember;
    }
}
