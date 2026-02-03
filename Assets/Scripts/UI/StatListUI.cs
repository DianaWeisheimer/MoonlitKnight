using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatListUI : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI staminaText;

    public void RefreshStatList()
    {
        //healthText.text = " Health: " + inventoryUI.character.stats.health.maxValue.ToString();
        //manaText.text = " Mana: " + inventoryUI.character.stats.mana.maxValue.ToString();
        //staminaText.text = " Stamina: " + inventoryUI.character.stats.stamina.maxValue.ToString();
    }
}
