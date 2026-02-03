using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    public PlayerInputEvents inputEvents;
    public UIInputEvents UIInputEvents;
    public QuestEvents questEvents;
    public PartyEvents partyEvents;
    public CombatEvents combatEvents;
    public UIEvents uIEvents;
    public PlayerMenuEvents playerMenuEvents;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }

        instance = this;

        questEvents = new QuestEvents();
        inputEvents = new PlayerInputEvents();
        UIInputEvents = new UIInputEvents();
        partyEvents = new PartyEvents();
        combatEvents = new CombatEvents();
        uIEvents = new UIEvents();
        playerMenuEvents = new PlayerMenuEvents();
    }
}
