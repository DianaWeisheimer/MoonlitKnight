using UnityEngine;
using System.Collections.Generic;

public class PartyHealthBarManager : MonoBehaviour
{
    public GameObject playerHealthBar;
    public Transform playerHealthBarParent;
    public GameObject companionHealthBar;
    public Transform companionBarContent;
    public List<PlayerHealthBar> spawnedHealthBars;
    private int activeMember;
    private bool isInitialized = false;

    private void Update()
    {
        if (isInitialized)
        {
            SetValues();
        }
    }

    private void SetValues()
    {
        for (int i = 0; i < PartyManager.instance.members.Count; i++)
        {
            spawnedHealthBars[i].SetValue(PartyManager.instance.GetMemberStats(i)); 
        }
    }

    private void SetHealthBars(List<PartyMember> members)
    {
        isInitialized = true;

        ClearCompanionBars();

        activeMember = PartyManager.instance.activeMember;

        for(int i = 0; i < PartyManager.instance.members.Count; i++)
        {
            if(i != activeMember)
            {
                GameObject healthBar = Instantiate(companionHealthBar);
                healthBar.transform.SetParent(companionBarContent);
                healthBar.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                healthBar.GetComponent<PlayerHealthBar>().SetBar(PartyManager.instance.GetMember(i).core.character);
                spawnedHealthBars.Add(healthBar.GetComponent<PlayerHealthBar>());
            }

            else
            {
                GameObject healthBar = Instantiate(playerHealthBar);
                healthBar.transform.SetParent(playerHealthBarParent);
                healthBar.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                healthBar.GetComponent<PlayerHealthBar>().SetBar(PartyManager.instance.GetMember(i).core.character);
                spawnedHealthBars.Add(healthBar.GetComponent<PlayerHealthBar>());
            }
        }
    }

    private void ClearCompanionBars()
    {
        if (spawnedHealthBars.Count != 0)
        {
            for (int i = 0; i < spawnedHealthBars.Count; i++)
            {
                GameObject buttonToDestroy = spawnedHealthBars[i].gameObject;
                Destroy(buttonToDestroy);
            }

            spawnedHealthBars.Clear();
        }
    }

    private void OnEnable()
    {
        GameEventsManager.instance.partyEvents.onPartyCreateMembers += SetHealthBars;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.partyEvents.onPartyCreateMembers -= SetHealthBars;
    }
}
