using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : PlayerMenuPage
{
    private int currentItemCategory;
    public GameObject itemButton;
    public List<GameObject> spawnedItemButton;
    public GameObject inventoryContent;
    public ItemDetailsPanel itemDetailsPanel;

    public override void OnPageOpen()
    {
        LoadItemCategory(currentItemCategory);
    }

    public void LoadItemCategory(int category)
    {
        ClearItemCategory();

        currentItemCategory = category;

        for (int i = 0; i < PartyManager.instance.inventory.slots.Count; i++)
        {
            if (PartyManager.instance.inventory.slots[i].item == null)
            {
                return;
            }

            if ((int)PartyManager.instance.inventory.slots[i].item.itemType == category)
            {
                GameObject button = Instantiate(itemButton, inventoryContent.transform);
                button.GetComponent<InventoryItemButtonUI>().SetButton(PartyManager.instance.inventory.slots[i], true);
                spawnedItemButton.Add(button);
            }
        }

        if(spawnedItemButton.Count == 0)
        {
            itemDetailsPanel.SetItemDetails(null);
        }

        else
        {
            itemDetailsPanel.SetItemDetails(spawnedItemButton[0].GetComponent<InventoryItemButtonUI>().slot);
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
}
