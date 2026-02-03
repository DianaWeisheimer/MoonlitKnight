using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot : MonoBehaviour
{
    /*public List<LootDrop> lootDrops;

    public void DropLoot()
    {
        for(int i = 0; i < lootDrops.Count; i++)
        {
            if(lootDrops[i].CheckDropChance() == true) { DropItem(lootDrops[i]); }
        }
    }
    private void DropItem(LootDrop lootDrop)
    {
        LootableObject loot = Instantiate(ItemManagerSingleton.Instance.lootableObject, transform.position, ItemManagerSingleton.Instance.lootableObject.transform.rotation);
        loot.BuildFromLootDrop(lootDrop);
    }*/
}
