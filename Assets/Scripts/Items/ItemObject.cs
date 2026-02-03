using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemObject : MonoBehaviour
{
    public abstract void Use();
    public abstract void CancelUse();
    public abstract void OnInstantiate(Character character, InventorySlot item);
}
