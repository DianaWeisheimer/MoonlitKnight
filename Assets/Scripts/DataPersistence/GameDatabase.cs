using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameDatabase", menuName = "Scriptable Objects/GameDatabase")]
public class GameDatabase : ScriptableObject
{
    public List<Item> items;
    public List<Job> jobs;
    public List<PartyMemberSO> partyMembers;
}
