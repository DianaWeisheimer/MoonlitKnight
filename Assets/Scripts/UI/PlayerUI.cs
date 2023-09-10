using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUI : MonoBehaviour
{   
    public GameObject crosshair;
    public TextMeshProUGUI goldAmmount;
    public Inventory playerInventory;

    public void ShowCrosshair(bool hehe)
    {
        crosshair.SetActive(hehe);
    }

    public void UpdateGoldAmmount(int gold)
    {
        goldAmmount.text = gold.ToString();
    }

    private void OnEnable()
    {
        PlayerMovementShooter.CrossHair += ShowCrosshair;
        GameEventManager.Instance.goldEvents.onGoldChange += UpdateGoldAmmount;
    }

    private void OnDisable()
    {
        PlayerMovementShooter.CrossHair -= ShowCrosshair;
        GameEventManager.Instance.goldEvents.onGoldChange -= UpdateGoldAmmount;
    }
}
