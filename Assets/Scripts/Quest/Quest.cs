using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfoSO questInfo;
    public QuestState questState;
    public int currentQuestStepIndex;

    public Quest(QuestInfoSO questInfo)
    {
        this.questInfo = questInfo;
        this.questState = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
    }

    public void MoveToNexrStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentQuestStepExists()
    {
        return (currentQuestStepIndex < questInfo.questStepPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();

        if(questStepPrefab != null)
        {
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>();
            questStep.InitializeQuestStep(questInfo.id);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;

        if(CurrentQuestStepExists()) 
        {
            questStepPrefab = questInfo.questStepPrefabs[currentQuestStepIndex];
        }

        return questStepPrefab;
    }
}
