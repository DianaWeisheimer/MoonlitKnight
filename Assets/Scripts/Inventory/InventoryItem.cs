using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    public PlayerInput playerInput;
    public InventorySlot slots;
    public Image image;
    public RectTransform rectTransform;
    public bool free;
    public Color32[] colors;
    public InventorySlot currentSlot;
    public InventorySlot newSlot;

    void Awake()
    {
        if (!playerInput)
        {
            playerInput = FindObjectOfType<PlayerInput>();
        }
    }

    public void LoadItem(Item itemToLoad)
    {
        item = itemToLoad;
        image.sprite = item.sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentSlot) { currentSlot.RemoveItem(); }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = playerInput.actions["Mouse"].ReadValue<Vector2>();
        PlayerInventoryController.inventoryItem = this;
        transform.position = new Vector3(playerInput.actions["Mouse"].ReadValue<Vector2>().x, playerInput.actions["Mouse"].ReadValue<Vector2>().y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (free)
        {
            EnterSlot();
        }

        else 
        {
            EnterCurrentSlot();
        }
    }

    public void EnterSlot()
    {
        transform.position = newSlot.gameObject.transform.position;
        if (currentSlot) { currentSlot.RemoveItem(); }
        currentSlot = newSlot;
        currentSlot.AddItem(this);
    }

    public void EnterCurrentSlot()
    {
        transform.position = currentSlot.gameObject.transform.position;
        if (currentSlot) { currentSlot.AddItem(this); }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InventorySlot"))
        {
            newSlot = collision.GetComponent<InventorySlot>();

            if (collision.GetComponent<InventorySlot>().item == null)
            {
                free = true;
                //image.color = colors[1];
            }

            else
            {
                free = false;
                //image.color = colors[0];
            }

            slots= collision.GetComponent<InventorySlot>();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InventorySlot"))
        {
            newSlot = null;
            free = false;
            image.color = colors[0];
            slots = null;
        }
    }
}
