using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType { Player, Companion, Enemy, NPC }

public class Character : MonoBehaviour
{
    public string characterName;
    public CharacterType type;
    public CharacterAnimation animations;
    public CharacterStats stats;
    public CharacterEquipment equipment;
    public CharacterInventory inventory;
    public CharacterStatusEffect statusEffects;
    public CharacterModel characterModel;
    public AbilityHolder abilityHolder;
    public AggroTable aggroTable;
    public bool hitable;
    public bool dead;

    private void Update()
    {
        statusEffects.Update();
    }

    private void FixedUpdate()
    {
        stats.Recharge();
    }

    public virtual void Initialize(CharacterData characterData, CharacterModel characterModel)
    {
        characterName = characterData.characterName;
        type = characterData.characterType;
        animations.animator = characterModel.animator;
        equipment.characterModel = characterModel;
        stats.baseStat = characterData.baseStats;
        this.characterModel = characterModel;

        stats.Initialize(this);
        abilityHolder.Initialize(this);
        statusEffects.Initialize(this);
        equipment.Initialize(this);
    }
    public virtual void EquipItem(InventorySlot item, EquipSlots slot)
    {

    }

    public virtual void UnequipItem(EquipSlots slot)
    {

    }

    public void TakeDamage()
    {

    }
}
