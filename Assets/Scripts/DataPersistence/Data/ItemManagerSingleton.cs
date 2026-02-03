using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagerSingleton : MonoBehaviour
{
    public static ItemManagerSingleton Instance;
    //public LootableObject lootableObject;
    public List<InventoryItem> items;
    public List<ConsumableItem> consumableItems;
    public List<WeaponItem> weaponItems;
    public List<ArmorItem> armorItems;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }
}
