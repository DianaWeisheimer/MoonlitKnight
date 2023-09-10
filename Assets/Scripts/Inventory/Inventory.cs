using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;
    public int size;
    public int gold;

    private void OnEnable()
    {
        GameEventManager.Instance.goldEvents.onGoldGained += GoldGained;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.goldEvents.onGoldGained -= GoldGained;
    }

    private void Start()
    {
        GameEventManager.Instance.goldEvents.GoldChange(gold);
    }

    private void GoldGained(int ammount)
    {
        gold += ammount;
        GameEventManager.Instance.goldEvents.GoldChange(gold);
    }
}
