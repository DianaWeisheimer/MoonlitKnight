using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PartyMemberCard : MonoBehaviour
{
    public static event Action<int> OnClick;
    public Image memberImage;
    public TextMeshProUGUI memberName;
    public TextMeshProUGUI memberJob;
    public int partyMemberIndex;
    public bool equipmentMenu;

    public void SetCard(PlayerCharacter member)
    {
        memberImage.sprite = member.memberIcon;
        memberName.text = member.characterName;

        if(member.job != null )
        {
            memberJob.text = member.job.jobName;
        }

        else
        {
            memberJob.text = "Jobless";
        }
    }

    public void OnButtonPress()
    {
        if(equipmentMenu)
        {
            OnClick?.Invoke(partyMemberIndex);
        }
    }
}
