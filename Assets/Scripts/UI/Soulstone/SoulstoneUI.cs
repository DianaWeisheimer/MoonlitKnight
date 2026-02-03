using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SoulstoneUI : MonoBehaviour
{
    public Soulstone soulstone;
    public GameObject mainMenu;
    public GameObject levelsMenu;
    public GameObject UI;
    public SoulstoneLevelingUI levelingUI;
    public SoulstoneJobUI jobUI;
    public GameObject firstSelected;

    public void OpenUI(Soulstone soulstone)
    {
        InputManager.instance.ChangeActionMap("UI");
        EventSystem.current.firstSelectedGameObject = firstSelected;
        this.soulstone = soulstone;
        UI.SetActive(true);
    }

    public void RestAtSoulstone()
    {
        CheckpointManager.instance.SetCheckpoint(soulstone.checkpoint);
        PartyManager.instance.GetActiveMember().core.GetComponent<Character>().stats.SetCurrentValue();
        EnemyManager.instance.ResetEnemies();
    }

    public void OpenLevels()
    {
        levelingUI.SetUI();
        //levelingUI.ResetLeveling();
        mainMenu.SetActive(false);
        levelsMenu.SetActive(true);
        jobUI.gameObject.SetActive(false);
    }

    public void CloseLevel()
    {
        //levelingUI.ResetLeveling();
        mainMenu.SetActive(true);
        levelsMenu.SetActive(false);
        jobUI.gameObject.SetActive(false);
    }

    public void OpenJob()
    {
        mainMenu.SetActive(false);
        levelsMenu.SetActive(false);
        jobUI.gameObject.SetActive(true);
    }

    public void Exit()
    {
        InputManager.instance.ChangeActionMap("Player");
        mainMenu.SetActive(true);
        levelsMenu.SetActive(false);
        jobUI.gameObject.SetActive(false);
        UI.SetActive(false);
    }

    private void OnEnable()
    {
        GameEventsManager.instance.uIEvents.onOpenSoulstoneMenu += OpenUI;
        GameEventsManager.instance.UIInputEvents.onCancelPressed += Exit;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.uIEvents.onOpenSoulstoneMenu -= OpenUI;
        GameEventsManager.instance.UIInputEvents.onCancelPressed -= Exit;
    }
}
