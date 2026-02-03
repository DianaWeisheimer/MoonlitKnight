using System;
using System.Numerics;

[Serializable]
public class PartyMemberData
{
    public int memberID;
    public Vector3 position;

    public PartyMemberData()
    {
        memberID = 0;
        position = Vector3.Zero;
    }
}
