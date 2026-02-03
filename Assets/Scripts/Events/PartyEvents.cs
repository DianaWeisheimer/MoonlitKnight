using System;
using System.Collections.Generic;

public class PartyEvents
{
    public event Action onPartyChangeMember;
    public void PartyChangeMember()
    {
        if (onPartyChangeMember != null)
        {
            onPartyChangeMember();
        }
    }

    public event Action<EquipSlots, int> onPartyUnequipItem;
    public void PartyUnequipItem(EquipSlots slot, int memberIndex)
    {
        if (onPartyUnequipItem != null)
        {
            onPartyUnequipItem(slot, memberIndex);
        }
    }

    public event Action onPartyLevelUpdate;
    public void PartyLevelUpdate()
    {
        if (onPartyLevelUpdate != null)
        {
            onPartyLevelUpdate();
        }
    }

    public event Action<InventorySlot> onPartyLootItem;
    public void PartyLootItem(InventorySlot item)
    {
        if (onPartyLootItem != null)
        {
            onPartyLootItem(item);
        }
    }

    public event Action<QuestInfoSO> onPartyClaimReward;
    public void PartyClaimReward(QuestInfoSO quest)
    {
        if (onPartyClaimReward != null)
        {
            onPartyClaimReward(quest);
        }
    }

    public event Action<int> onPartyGainXP;
    public void PartyGainXP(int xp)
    {
        if (onPartyGainXP != null)
        {
            onPartyGainXP(xp);
        }
    }

    public event Action<Job> onPartyChangeJob;
    public void PartyChangeJob(Job job)
    {
        if (onPartyChangeJob != null)
        {
            onPartyChangeJob(job);
        }
    }

    public event Action<List<PartyMember>> onPartyCreateMembers;
    internal void PartyCreateMembers(List<PartyMember> members)
    {
        onPartyCreateMembers?.Invoke(members);
    }
}
