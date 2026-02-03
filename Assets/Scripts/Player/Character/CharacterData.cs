using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public BaseStat baseStats;
    public CharacterType characterType;
    public GameObject characterModel;
    public AIBrain brain;
}
