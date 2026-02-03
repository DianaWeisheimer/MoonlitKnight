using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public class PartyManager : MonoBehaviour, IDataPersistence
{
    public static PartyManager instance { get; private set; }

    public List<PartyMember> members;
    public GameObject characterCore;
    public int activeMember;
    public float partyXP;

    public PartyInventory inventory;

    public bool createPartyOnStart;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Party Manager in the scene.");
        }

        instance = this;

        //CheckActiveMember();
    }

    void Start()
    {
        if (createPartyOnStart)
        {
            CreateParty();
        }

        //Time.timeScale = 0.1f;
    }

    public void CreateParty()
    {
        //SetActiveMember(0);
        for (int i = 0; i < members.Count; i++)
        {
            if (members[i].characterData == null)
            {
                Debug.LogError("Party member at index " + i + " is null. Please assign a valid PartyMember.");
                return;
            }

            if (members[i].core == null)
            {
                Transform spawnPosition = SoulstoneManager.instance.soulstones[0].transform;
                members[i].core = Instantiate(characterCore, spawnPosition.position, Quaternion.identity).GetComponent<CharacterCore>();
                members[i].core.characterData = members[i].characterData;
                members[i].core.ApplyData(members[i].characterData);
                members[i].core.SetRole(members[i].role);
            }
        }

        GameEventsManager.instance.partyEvents.PartyCreateMembers(members);
    }
    public void SetActiveMember(int index)
    {
        if (index < 0 || index >= members.Count)
        {
            Debug.LogError("Invalid active member index.");
            return;
        }

        // Set current player to companion
        foreach (var member in members)
        {
            if (member.core != null && member.core.currentRole == CharacterRole.Player)
            {
                member.core.SetRole(CharacterRole.Companion);
                member.role = CharacterRole.Companion;
            }
        }

        // Set new active member
        activeMember = index;
        var newActive = members[activeMember];

        if (newActive.core != null)
        {
            newActive.core.SetRole(CharacterRole.Player);
            newActive.role = CharacterRole.Player;
        }
    }
    public void SwitchPlayer()
    {
        activeMember++;

        if(activeMember >= members.Count)
        {
            activeMember = 0;
        }

        //CheckActiveMember();

        GameEventsManager.instance.partyEvents.PartyChangeMember();
    }

    private void LootItem(InventorySlot item)
    {
        inventory.AddItem(item);
    }

    public PartyMember GetMember(int index)
    {
        if (!ValidateMember(index))
            return null;

        return members[index];
    }

    public PartyMember GetActiveMember()
    {
        return members[activeMember];
    }

    public Job GetMemberJob(int index)
    {
        if (!ValidateMember(index))
            return null;

        return members[index].core.character.job;
    }

    public Job GetActiveMemberJob()
    {
        return members[activeMember].core.character.job;
    }

    public CharacterStats GetMemberStats(int index)
    {
        if(!ValidateMember(index))
            return null;

        return members[index].core.character.stats;
    }

    private bool ValidateMember(int index)
    {
        if (index < 0 || index >= members.Count)
        {
            Debug.LogError("Invalid member index: " + index);
            return false;
        }

        return true;
    }

    private void GainXP(int xpAmmount)
    {
        for (int i = 0; i < members.Count; i++)
        {
            members[i].core.character.stats.AddXP(xpAmmount);
        }
    }

    private void ClaimReward(QuestInfoSO quest)
    {
        for(int i  = 0; i < members.Count; i++)
        {
            members[i].core.character.stats.AddXP(quest.xpReward);
        }

        if(quest.itemReward != null)
        {
            for (int i = 0; i < quest.itemReward.Length; i++)
            {
                //inventory.AddItem(quest.itemReward[i]);
            }
        }
    }

    public InventorySlot LookForItem(string itemName, ItemCategory itemType)
    {
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i].item.itemName == itemName && inventory.slots[i].item.itemType == itemType)
            {
                return inventory.slots[i];
            }
        }

        return null;
    }

    public void EquipItem(InventorySlot item, EquipSlots slot, int memberIndex)
    {
        switch (slot){
            case EquipSlots.Consumable1:
                if (item.item is ConsumableItem)
                {
                    inventory.EquipConsumable(item, 0);
                }
                break;
            case EquipSlots.Consumable2:
                if (item.item is ConsumableItem)
                {
                    inventory.EquipConsumable(item, 1);
                }
                break;
            case EquipSlots.Consumable3:
                if (item.item is ConsumableItem)
                {
                    inventory.EquipConsumable(item, 2);
                }
                break;
            default:
                item.isEquipped = true;
                item.equippedBy = members[memberIndex].core.character;
                members[memberIndex].core.character.EquipItem(item, slot);
                break;
        }
    }

    public void UnequipItem(EquipSlots slot, int memberIndex)
    {
        switch (slot)
        {
            case EquipSlots.RightHand:
                for (int i = 0; i < inventory.slots.Count; i++)
                {
                    if (inventory.slots[i] == members[memberIndex].core.character.inventory.rightHand)
                    {
                        inventory.slots[i].isEquipped = false;
                    }
                }
                break;

            case EquipSlots.LeftHand:
                for (int i = 0; i < inventory.slots.Count; i++)
                {
                    if (inventory.slots[i] == members[memberIndex].core.character.inventory.leftHand)
                    {
                        inventory.slots[i].isEquipped = false;
                    }
                }
                break;

            case EquipSlots.Head:
                for (int i = 0; i < inventory.slots.Count; i++)
                {
                    if (inventory.slots[i] == members[memberIndex].core.character.inventory.head)
                    {
                        inventory.slots[i].isEquipped = false;
                    }
                }
                break;

            case EquipSlots.Body:
                for (int i = 0; i < inventory.slots.Count; i++)
                {
                    if (inventory.slots[i] == members[memberIndex].core.character.inventory.body)
                    {
                        inventory.slots[i].isEquipped = false;
                    }
                }
                break;

            case EquipSlots.Accessory1:
                for (int i = 0; i < inventory.slots.Count; i++)
                {
                    if (inventory.slots[i] == members[memberIndex].core.character.inventory.accessory1)
                    {
                        inventory.slots[i].isEquipped = false;
                    }
                }
                break;

            case EquipSlots.Accessory2:
                for (int i = 0; i < inventory.slots.Count; i++)
                {
                    if (inventory.slots[i] == members[memberIndex].core.character.inventory.accessory2)
                    {
                        inventory.slots[i].isEquipped = false;
                    }
                }
                break;
        }

        members[memberIndex].core.character.UnequipItem(slot);

        if(memberIndex == activeMember)
        {
            PlayerMovement mov = members[memberIndex].core.GetComponentInChildren<PlayerMovement>();
            mov.CheckMovementType();
        }
    }

    private void OnEnable()
    {
        GameEventsManager.instance.partyEvents.onPartyClaimReward += ClaimReward;
        GameEventsManager.instance.partyEvents.onPartyLootItem += LootItem;
        GameEventsManager.instance.partyEvents.onPartyGainXP += GainXP;
        GameEventsManager.instance.playerMenuEvents.onEquipItemPressed += EquipItem;
        GameEventsManager.instance.playerMenuEvents.onUnequipItemPressed += UnequipItem;
        GameEventsManager.instance.inputEvents.onChangeCharacterPressed += SwitchPlayer;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.partyEvents.onPartyClaimReward -= ClaimReward;
        GameEventsManager.instance.partyEvents.onPartyLootItem -= LootItem;
        GameEventsManager.instance.partyEvents.onPartyGainXP -= GainXP;
        GameEventsManager.instance.playerMenuEvents.onEquipItemPressed -= EquipItem;
        GameEventsManager.instance.playerMenuEvents.onUnequipItemPressed -= UnequipItem;
        GameEventsManager.instance.inputEvents.onChangeCharacterPressed -= SwitchPlayer;
    }

    public void LoadData(GameData data)
    {
        //inventory.LoadFromPartyInventoryData(data.partyData.partyInventoryData);

        if(createPartyOnStart)
        {
            
        }
    }

    public void SaveData(GameData data)
    {
        data.partyData.partyInventoryData.SaveFromInventory(inventory);
    }

}
