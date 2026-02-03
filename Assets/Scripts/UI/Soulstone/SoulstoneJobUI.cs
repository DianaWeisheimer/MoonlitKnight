using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulstoneJobUI : MonoBehaviour
{
    

    public void SetUI()
    {
        

        RefreshUI();
    }

    public void SelectJob(Job job)
    {
        PartyManager.instance.GetActiveMember().ChangeJob(job);
    }  

    private void RefreshUI()
    {
        
    }
}
