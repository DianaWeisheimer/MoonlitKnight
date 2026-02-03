using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> _questMap;

    private int _playerLevel;

    private void Awake()
    {
        _questMap = CreateQuestMap();
    }

    private void Start()
    {
        foreach(Quest quest in _questMap.Values)
        {
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    private void Update()
    {
        foreach(Quest quest in _questMap.Values )
        {
            if(quest.questState == QuestState.REQUIREMENTS_NOT_MET && CheckRequirements(quest))
            {
                ChangeQuestState(quest.questInfo.id, QuestState.CAN_START);
            }
        }
    }

    private void PlayerLevelChanged(CharacterLevel characterLevel)
    {
        _playerLevel = characterLevel.GetCharacterLevel();
    }

    private bool CheckRequirements(Quest quest)
    {
        bool metRequirements = true;

        if(_playerLevel < quest.questInfo.levelRequirement)
        {
            metRequirements = false;
        }

        foreach(QuestInfoSO questPrerequisites in quest.questInfo.questPrerequisets)
        {
            if(GetQuestById(questPrerequisites.id).questState != QuestState.FINISHED)
            {
                metRequirements = false;
            }
        }

        return metRequirements;
    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.questState = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.questInfo.id, QuestState.IN_PROGRESS);
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.MoveToNexrStep();
        if (quest.CurrentQuestStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }

        else
        {
            ChangeQuestState(id, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        ClaimRewards(quest);
        ChangeQuestState(id, QuestState.FINISHED);
    }

    private void ClaimRewards(Quest quest)
    {
        GameEventsManager.instance.partyEvents.PartyClaimReward(quest.questInfo);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();

        foreach(QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate Quest ID");
            }

            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }

        return idToQuestMap;
    }

    private Quest GetQuestById(string id)
    {
        Quest quest = _questMap[id];

        if(quest == null)
        {
            Debug.LogWarning("Id not found in quest map" + id);

        }

        return quest;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.OnStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.OnAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.OnFinishQuest += FinishQuest;

        CharacterLevel.OnLevelChange += PlayerLevelChanged;
    }

    private void OnDisnable()
    {
        GameEventsManager.instance.questEvents.OnStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.OnAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.OnFinishQuest -= FinishQuest;

        CharacterLevel.OnLevelChange -= PlayerLevelChanged;
    }
}
