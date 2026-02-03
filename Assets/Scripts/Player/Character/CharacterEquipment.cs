using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterEquipment
{
    private Character character;
    public CharacterModel characterModel;
    public ItemObject rightHandItem;
    public ItemObject leftHandItem;
    public event Action onBlockSuccess;

    public void Initialize(Character _character)
    {
        character = _character;
    }

    public void EquipItem(InventorySlot item, EquipSlots slots)
    {
        if(item.item is WeaponItem && slots == EquipSlots.RightHand)
        {
            EquipRightHand(item.item as WeaponItem, item.modifier);
        }

        if (item.item is WeaponItem && slots == EquipSlots.LeftHand)
        {
            EquipLeftHand(item.item as WeaponItem, item.modifier);
        }

        else if(item.item is ArmorItem)
        {
            EquipArmor(item.item as ArmorItem, item.modifier);
        }
    }

    public void UnequipItem(EquipSlots slots)
    {
        switch (slots)
        {
            case EquipSlots.RightHand:
                if (rightHandItem == null) return;
                GameObject rightHand = rightHandItem.gameObject;
                GameObject.Destroy(rightHand);
                rightHandItem = null;
                break;
            case EquipSlots.LeftHand:
                if (leftHandItem == null) return;
                GameObject leftHand = leftHandItem.gameObject;
                GameObject.Destroy(leftHand);
                leftHandItem = null;
                break;
        }
    }

    private void EquipRightHand(WeaponItem weapon, List<ItemModifier> modifiers)
    {
        /*switch (weapon.category)
        {
            case WeaponCategory.Sword:
                rightHandItem = GameObject.Instantiate(weapon.weaponObject, characterModel.handPos[0]);
                break;
        }*/
        if (rightHandItem)
        {
            UnequipItem(EquipSlots.RightHand);
        }

        rightHandItem = GameObject.Instantiate(weapon.weaponObject, characterModel.handPos[0]);
        rightHandItem.OnInstantiate(character, character.inventory.rightHand);
    }

    public void UnequipRightHand()
    {

    }

    private void EquipLeftHand(WeaponItem weapon, List<ItemModifier> modifiers)
    {
        /*switch (weapon.category)
        {
            case WeaponCategory.Dagger:
                leftHandItem = GameObject.Instantiate(weapon.weaponObject, characterModel.handPos[1]);
                break;
        }*/

        if (leftHandItem)
        {
            UnequipItem(EquipSlots.LeftHand);
        }

        leftHandItem = GameObject.Instantiate(weapon.weaponObject, characterModel.handPos[1]);
        leftHandItem.OnInstantiate(character, character.inventory.leftHand);
    }

    public void UnequipLeftHand()
    {

    }

    private void EquipArmor(ArmorItem armor, List<ItemModifier> modifiers) 
    {

    }

    public void UnequipArmor()
    {

    }

    public void SetWeaponCollider(bool hehe, bool rightHand)
    {
        switch (rightHand)
        {
            case true:
                if (rightHandItem && rightHandItem is WeaponObject)
                {
                    WeaponObject weaponObject = (WeaponObject)rightHandItem;

                    weaponObject.SetCollider(hehe);
                }
                break;

            case false:
                if (leftHandItem && leftHandItem is WeaponObject)
                {
                    WeaponObject weaponObject = (WeaponObject)leftHandItem;

                    weaponObject.SetCollider(hehe);
                }
                if (leftHandItem && leftHandItem is ShieldObject)
                {
                    WeaponObject weaponObject = (ShieldObject)leftHandItem;

                    weaponObject.SetCollider(hehe);
                }
                break;
        }
    }

    public void SetWeaponBlock(bool rightHand, bool hehe)
    {
        //Debug.Log("Set Weapon Block");
        switch (rightHand)
        {
            case true:
                if (rightHandItem && rightHandItem is WeaponObject)
                {
                    WeaponObject weaponObject = (WeaponObject)rightHandItem;

                    weaponObject.SetBlocking(hehe);
                }
                break;

            case false:
                if (leftHandItem && leftHandItem is WeaponObject)
                {
                    WeaponObject weaponObject = (WeaponObject)leftHandItem;

                    weaponObject.SetBlocking(hehe);
                }
                break;
        }
    }

    public void CalculateWeaponDamage(float multiplier)
    {
        if(rightHandItem is WeaponObject)
        {
            WeaponObject weapon = (WeaponObject)rightHandItem;
            weapon.CalculateDamage(character.inventory.rightHand, character, 0, multiplier);
        }

        if (leftHandItem is WeaponObject)
        {
            WeaponObject weapon = (WeaponObject)leftHandItem;
            weapon.CalculateDamage(character.inventory.leftHand, character, 0, multiplier);
        }
    }

    public void OnBlockSuccess()
    {
        onBlockSuccess?.Invoke();
    }
}
