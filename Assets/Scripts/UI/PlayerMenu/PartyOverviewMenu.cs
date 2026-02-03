using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PartyOverviewMenu : PlayerMenuPage
{
    public Transform partyMemberCardParent;
    public GameObject partyMemberCard;
    public List<GameObject> spawnedPartyMemberCards;

    public override void OnPageOpen()
    {
        RefreshInformation();
    }

    public void RefreshInformation()
    {
        ClearPartyMemberCards();

        for (int i = 0; i < PartyManager.instance.members.Count; i++)
        {
            GameObject card = Instantiate(partyMemberCard);
            card.transform.SetParent(partyMemberCardParent);
            card.transform.localScale = Vector3.one;
            card.GetComponent<PartyMemberCard>().SetCard(PartyManager.instance.GetMember(i).core.character); 
            
            spawnedPartyMemberCards.Add(card);
        }
    }

    public void ClearPartyMemberCards()
    {
        if (spawnedPartyMemberCards.Count != 0)
        {
            for (int i = 0; i < spawnedPartyMemberCards.Count; i++)
            {
                GameObject buttonToDestroy = spawnedPartyMemberCards[i];
                Destroy(buttonToDestroy);
            }

            spawnedPartyMemberCards.Clear();
        }
    }
}
