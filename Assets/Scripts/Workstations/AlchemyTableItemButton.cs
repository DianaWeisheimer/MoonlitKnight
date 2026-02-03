using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlchemyTableItemButton : MonoBehaviour
{
    /*private AlchemyTable _alchemyTable;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemAmmount;
    public Image itemImage;
    public InventoryItem item;
    private bool _ingredient;
    private int _ingredientIndex;

    public void SetButton(InventoryItem inventoryItem, AlchemyTable table, bool isIngredient, int ingredientIndex)
    {
        _alchemyTable = table;
        _ingredient = isIngredient;
        _ingredientIndex = ingredientIndex;

        item = inventoryItem;
        itemName.text = inventoryItem.item.itemName;

        if (item.item.itemImage)
        {
            itemImage.sprite = item.item.itemImage;
        }

        if (inventoryItem.item.stackable)
        {
            itemAmmount.text = "x" + inventoryItem.stackAmmount.ToString();
        }

        else
        {
            itemAmmount.gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        if (_ingredient) _alchemyTable.SelectIngredient(item, _ingredientIndex);
        else _alchemyTable.SelectBase(item);
    }*/
}
