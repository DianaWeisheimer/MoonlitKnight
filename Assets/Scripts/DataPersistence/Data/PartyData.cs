using System;
using System.Collections.Generic;

[Serializable]
public class PartyData
{
    public List<PartyMemberData> memberData;
    public PartyInventoryData partyInventoryData;
    public PartyData()
    {
        memberData = new List<PartyMemberData>();
        //memberData[0] = new PartyMemberData();
        partyInventoryData = new PartyInventoryData();
    }
}

