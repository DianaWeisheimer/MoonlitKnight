using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfoSO info;
    public QuestState state;
    public int currentQuestStepIndex;

    public Quest(QuestInfoSO questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        currentQuestStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentQuestStepExists()
    {
        return (currentQuestStepIndex < info.questStepPrefab.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();

        if(questStepPrefab != null)
        {
            Object.Instantiate<GameObject>(questStepPrefab, parentTransform);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;

        if (CurrentQuestStepExists())
        {
            questStepPrefab = info.questStepPrefab[currentQuestStepIndex];
        }

        else
        {
            Debug.LogWarning("Tried to get quest step prefab, but step index was out of range" + "quest ID" + info.id + "step ID" + currentQuestStepIndex);
        }

        return questStepPrefab;
    }
}
