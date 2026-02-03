using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUpdateUI : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI ammount;
    public void SetButton(InventoryItem item)
    {
        animator.SetTrigger("Open");
        itemName.text = item.item.itemName;
        ammount.text = item.stackAmmount.ToString() + "x";
        Destroy(gameObject, 2f);
    }
}
