using UnityEngine;

[CreateAssetMenu(fileName = "PartyMemberSO", menuName = "Scriptable Objects/PartyMemberSO")]
public class PartyMemberSO : ScriptableObject
{
    public int partyMemberID;
    public GameObject characterObject;
}
