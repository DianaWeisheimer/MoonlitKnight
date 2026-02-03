using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [SerializeField] private GameObject startActiveIcon;
    [SerializeField] private GameObject startInactiveIcon;
    [SerializeField] private GameObject finishActiveIcon;
    [SerializeField] private GameObject finishInactiveIcon;

    public void SetState(QuestState state, bool startPoint, bool finishPoint)
    {
        startActiveIcon.SetActive(false);
        startInactiveIcon.SetActive(false);
        finishActiveIcon.SetActive(false);
        finishInactiveIcon.SetActive(false);

        switch(state)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
                if (startPoint) { startInactiveIcon.SetActive(true); }
                break;
            case QuestState.CAN_START:
                if (startPoint) { startActiveIcon.SetActive(true); }
                break;
            case QuestState.IN_PROGRESS:
                if (finishPoint) { finishInactiveIcon.SetActive(true); }
                break;
            case QuestState.CAN_FINISH:
                if (finishPoint) { finishActiveIcon.SetActive(true); }
                break;
            case QuestState.FINISHED:
                break;
        }
    }
}
