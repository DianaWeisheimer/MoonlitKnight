using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulstoneCursor : MonoBehaviour
{
    public SoulTreeSphere soulTreeSphere;
    public SoulstoneLevelingUI levelingUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SoulSphere"))
        {
            soulTreeSphere = collision.GetComponent<SoulTreeSphere>();
            //levelingUI.SetSphereDetails(soulTreeSphere);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SoulSphere"))
        {
            soulTreeSphere = null;
            //levelingUI.SetSphereDetails(null);
        }
    }
}
