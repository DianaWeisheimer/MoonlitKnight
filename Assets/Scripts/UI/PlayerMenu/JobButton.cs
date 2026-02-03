using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JobButton : MonoBehaviour
{
    public TextMeshProUGUI jobName;     
    public Job job;
    public int partyMemberIndex;
    public void SetButton(Job newJob)
    {
        job = newJob;
        jobName.text = job.name;
    }

    public void ChangeCharacterJob()
    {

    }
}
