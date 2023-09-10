using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour
{
    //public List<InventorySlot> slots;
    public bool free;
    public InventoryItem InventoryItem;
    public Color32[] colors;
    public Image image;
    public InventorySlot currentSlot;
    public InventorySlot newSlot;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InventorySlot"))
        {
            newSlot = collision.GetComponent<InventorySlot>();

            if (collision.GetComponent<InventorySlot>().item == null)
            {
                free = true;
                image.color = colors[1];
            }

            else
            {
                free = false;
                image.color = colors[0];
            }
            //slots.Add(collision.GetComponent<InventorySlot>());
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InventorySlot"))
        {
            newSlot = null;
            free = false;
            image.color = colors[0];
            //slots.Remove(other.GetComponent<InventorySlot>());
        }
    }

    public void BeginDrag()
    {
        if (currentSlot) { currentSlot.RemoveItem(); }
    }

    public void EndDrag()
    {
        if (currentSlot) { currentSlot.AddItem(InventoryItem); }
    }

    public void EnterSlot()
    {
        if (currentSlot) { currentSlot.RemoveItem(); }
        currentSlot = newSlot;
        currentSlot.AddItem(InventoryItem);
    }
}
