using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    public GameObject menu;
    public List<GameObject> pages;
    public int pageIndex;


    public void OpenMenu(bool hehe)
    {
        menu.SetActive(true);
        InputManager.instance.ChangeActionMap("UI");

        CheckActiveMenu();
    }

    public void CloseMenu()
    {
        if(CloseSubMenus() == true)
        {
            return;
        }

        else if(CloseSubMenus() == false)
        {
            menu.SetActive(false);
            InputManager.instance.ChangeActionMap("Player");
        }
    }

    public bool CloseSubMenus()
    {
        if (pages[pageIndex].GetComponent<PlayerMenuPage>().CloseSubMenu() == true)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public void PageButton(int index)
    {
        pageIndex = index;

        CheckActiveMenu();
    }

    private void PageRight()
    {
        if(menu.activeSelf == false) return;
        pageIndex++;
        pageIndex = Mathf.Clamp(pageIndex, 0, pages.Count - 1);
        CheckActiveMenu();
    }

    private void PageLeft()
    {
        pageIndex--;
        pageIndex = Mathf.Clamp(pageIndex, 0, pages.Count - 1);
        CheckActiveMenu();
    }

    private void CheckActiveMenu()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            if (i != pageIndex) { pages[i].SetActive(false); }

            else if (i == pageIndex) 
            { 
                pages[i].SetActive(true); 
                pages[i].GetComponent<PlayerMenuPage>().OnPageOpen(); 
            }
        }

        /*switch (pageIndex)
        {
            case 0:
                EventSystem.current.firstSelectedGameObject = firstSelectedInventory;
                break;
            case 1:
                EventSystem.current.firstSelectedGameObject = firstSelectedEquipment;
                break;
            case 3:
                EventSystem.current.firstSelectedGameObject = firstSelectedSettings;
                break;
        }*/
    }

    private void OnEnable()
    {
        GameEventsManager.instance.UIInputEvents.onCancelPressed += CloseMenu;
        GameEventsManager.instance.UIInputEvents.onPageRightPressed += PageRight;
        GameEventsManager.instance.UIInputEvents.onPageLeftPressed += PageLeft;

        //player.character.stats.Died += CloseInventory;
        PlayerMovement.OpenInventory += OpenMenu;
    }


    private void OnDisable()
    {
        GameEventsManager.instance.UIInputEvents.onCancelPressed -= CloseMenu;
        GameEventsManager.instance.UIInputEvents.onPageRightPressed -= PageRight;
        GameEventsManager.instance.UIInputEvents.onPageLeftPressed -= PageLeft;

        //player.character.stats.Died -= CloseInventory;
        PlayerMovement.OpenInventory -= OpenMenu;
    }
}
