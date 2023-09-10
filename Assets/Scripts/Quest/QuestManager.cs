using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        GameEventManager.Instance.questEvents.onStartQuest += StartQuest;
        GameEventManager.Instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventManager.Instance.questEvents.onFinishQuest += FinishQuest;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.questEvents.onStartQuest -= StartQuest;
        GameEventManager.Instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventManager.Instance.questEvents.onFinishQuest -= FinishQuest;
    }

    private void Start()
    {
        foreach(Quest quest in questMap.Values)
        {
            GameEventManager.Instance.questEvents.QuestStateChange(quest);
        }
    }

    private void StartQuest(string id)
    {

    }

    private void AdvanceQuest(string id)
    {

    }

    private void FinishQuest(string id)
    {

    }

    public Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allQuest = Resources.LoadAll<QuestInfoSO>("Quests"); 

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();

        foreach(QuestInfoSO questInfo in allQuest)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.id);
            }

            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }

        return idToQuestMap;
    }

    private Quest GetQuestByID(string id)
    {
        Quest quest = questMap[id];

        if(quest == null)
        {
            Debug.LogWarning("No ID found: " + id);
        }

        return quest;
    }
}
