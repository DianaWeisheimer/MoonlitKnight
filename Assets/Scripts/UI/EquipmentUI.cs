using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentUI : MonoBehaviour
{
    public List<Image> equipImages;
    public List<Image> abilityImages;
    public List<Image> abilityManaOverlay;
    public List<Image> abilityCooldownOverlay;
    public List<TextMeshProUGUI> abilityCooldownText;
    public Material abilityMaterial;
    public Material abilityGreyscaleMaterial;
    
    public void RefreshImages(PartyInventory inventory)
    {
        //if(playerInventory.equippedItems[17].item != null) equipImages[0].sprite = playerInventory.equippedItems[17].item.itemImage;

        //if (inventory.equippedItems[inventory.activeRightHand].item != null)
        //{
            //equipImages[1].sprite = inventory.equippedItems[inventory.activeRightHand].item.itemImage;
        //}

        //if(playerInventory.equippedItems[8].item != null) equipImages[2].sprite = playerInventory.equippedItems[8].item.itemImage;
        //if(playerInventory.equippedItems[2].item != null) equipImages[3].sprite = playerInventory.equippedItems[2].item.itemImage;
    }

    public void RefreshAbilityIcons(AbilityHolder abilityHolder)
    {
        for (int i = 0; i < abilityImages.Count && i < abilityHolder.ability.Count; i++)
        {
            var state = abilityHolder.ability[i].abilityState;
            var cooldown = abilityHolder.ability[i].cooldownTime;

            abilityImages[i].sprite = abilityHolder.ability[i].abilitySO.abilityIcon;
            abilityCooldownOverlay[i].gameObject.SetActive(false);
            abilityManaOverlay[i].gameObject.SetActive(false);
            abilityImages[i].material = null;

            switch (state)
            {
                case AbilityState.cooldown:
                    abilityCooldownOverlay[i].gameObject.SetActive(true);
                    abilityCooldownText[i].text = cooldown.ToString("0");
                    break;

                case AbilityState.no_resources:
                    abilityImages[i].material = abilityGreyscaleMaterial;
                    abilityManaOverlay[i].gameObject.SetActive(true);
                    break;

                case AbilityState.no_nothing:
                    abilityImages[i].material = abilityGreyscaleMaterial;
                    abilityManaOverlay[i].gameObject.SetActive(true);
                    abilityCooldownOverlay[i].gameObject.SetActive(true);
                    abilityCooldownText[i].text = cooldown.ToString("0");
                    break;
            }
        }
    }

    private void OnEnable()
    {
        GameEventsManager.instance.uIEvents.onUpdateAbilities += RefreshAbilityIcons;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.uIEvents.onUpdateAbilities -= RefreshAbilityIcons;
    }
}
