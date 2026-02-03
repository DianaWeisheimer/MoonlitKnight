using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPoint : MonoBehaviour
{
    [SerializeField] private QuestInfoSO quest;
    private string questID;
    private QuestState currentQuestState;
    public QuestIcon questIcon;

    [Header("Config")]
    [SerializeField] private bool _startPoint;
    [SerializeField] private bool _finishPoint;
    [SerializeField] private bool _onInteract;

    private void Awake()
    {
        questID = quest.id;
    }

    public void Interact(bool hehe, Player player)
    {
        if(_onInteract)
        {
            if(currentQuestState == QuestState.CAN_START && _startPoint) 
            {
                GameEventsManager.instance.questEvents.StartQuest(questID);
            }

            else if(currentQuestState == QuestState.CAN_FINISH && _finishPoint)
            {
                GameEventsManager.instance.questEvents.FinishQuest(questID);
            }
        }
    }

    public void EnteredTrigger()
    {
        if (!_onInteract)
        {
            if (currentQuestState == QuestState.CAN_START && _startPoint)
            {
                GameEventsManager.instance.questEvents.StartQuest(questID);
            }

            else if (currentQuestState == QuestState.CAN_FINISH && _finishPoint)
            {
                GameEventsManager.instance.questEvents.FinishQuest(questID);
            }
        }
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.OnQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.OnQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        if(quest.questInfo.id == questID)
        {
            currentQuestState = quest.questState;

            if (questIcon) { questIcon.SetState(quest.questState, _startPoint, _finishPoint); }
        }
    }
}
