using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest")]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;
    public string description;

    [Header("Requirements")]
    public int playerLevel;
    public QuestInfoSO[] questPrerequisits;

    [Header("Steps")]
    public GameObject[] questStepPrefab;

    [Header("Rewards")]
    public int gold;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

    public string questName;
    public string questDescription;
    public QuestObjective[] objectives;
}
