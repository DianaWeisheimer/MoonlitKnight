using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailsPanel : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public Sprite emptyItemSprite;

    public void SetItemDetails(InventorySlot slot)
    {
        if(slot == null)
        {
            itemImage.sprite = emptyItemSprite;
            itemName.text = "";
            itemDescription.text = "";
        }

        else
        {
            itemImage.sprite = slot.item.itemImage;
            itemName.text = slot.item.itemName;
            itemDescription.text = slot.item.itemDescription;
        }
    }

    private void OnEnable()
    {
        GameEventsManager.instance.playerMenuEvents.onOpenItemDetails += SetItemDetails;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerMenuEvents.onOpenItemDetails -= SetItemDetails; 
    }
}
