using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyTable : MonoBehaviour
{
    /*public Character character;
    public GameObject craftingTab;
    public GameObject itemsTab;
    public Transform itemsTabContent;
    public GameObject itemButton;
    public List<GameObject> spawnedItemButton;
    public bool openedSubMenu;

    [SerializeField]
    private InventoryItem[] _ingredients;

    [SerializeField]
    private InventoryItem _base;

    [SerializeField]
    private Transform[] _ingredientPos;

    [SerializeField]
    private ConsumableObject[] _ingredientObjects;

    public void LoadIngredientsTab(int ingredientsIndex)
    {
        openedSubMenu = true;
        craftingTab.SetActive(false);
        itemsTab.SetActive(true);
        LoadIngredients(ingredientsIndex);
    }

    public void LoadBasesTab()
    {
        openedSubMenu = true;
        craftingTab.SetActive(false);
        itemsTab.SetActive(true);
        LoadBases();
    }

    public void CloseItemsTab()
    {
        openedSubMenu = false;
        ClearItemCategory();

        craftingTab.SetActive(true);
        itemsTab.SetActive(false);
    }

    public void LoadIngredients(int ingredientIndex)
    {
        ClearItemCategory();

        for (int i = 0; i < character.inventory.items.Count; i++)
        {
            if (character.inventory.items[i].item == null)
            {
                return;
            }

            if (character.inventory.items[i].item.itemType == ItemCategory.Consumable)
            {
                ConsumableItem consumableItem = character.inventory.items[i].item as ConsumableItem;

                if (consumableItem.potionIngredient)
                {
                    GameObject button = Instantiate(itemButton, itemsTabContent.transform);
                    button.GetComponent<AlchemyTableItemButton>().SetButton(character.inventory.items[i], this, true, ingredientIndex);
                    spawnedItemButton.Add(button);
                }
            }
        }
    }

    public void LoadBases()
    {
        ClearItemCategory();

        for (int i = 0; i < character.inventory.items.Count; i++)
        {
            if (character.inventory.items[i].item == null)
            {
                return;
            }

            if (character.inventory.items[i].item.itemType == ItemCategory.Consumable)
            {
                ConsumableItem consumableItem = character.inventory.items[i].item as ConsumableItem;

                if (consumableItem.potionBase)
                {
                    GameObject button = Instantiate(itemButton, itemsTabContent.transform);
                    button.GetComponent<AlchemyTableItemButton>().SetButton(character.inventory.items[i], this, false, 0);
                    spawnedItemButton.Add(button);
                }
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

    private void UpdateIngredienObjects(int ingredientIndex)
    {
        if(_ingredientObjects[ingredientIndex] != null)
        {
            Destroy(_ingredientObjects[ingredientIndex]);
        }

        ConsumableItem consumableItem = _ingredients[ingredientIndex].item as ConsumableItem;
        _ingredientObjects[ingredientIndex] = Instantiate(consumableItem.consumableObject, _ingredientPos[ingredientIndex].position, _ingredientPos[ingredientIndex].rotation);     
    }

    public void SelectIngredient(InventoryItem item, int ingredientIndex)
    {
        _ingredients[ingredientIndex] = item;
        UpdateIngredienObjects(ingredientIndex);
        CloseItemsTab();
    }

    public void SelectBase(InventoryItem item)
    {
        _base = item;
        CloseItemsTab();
    }*/
}
