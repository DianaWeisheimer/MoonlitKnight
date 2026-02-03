using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulTreeSphere : MonoBehaviour
{
    public SoultTreeSphereInfoSO infoSO;
    public int requiredFae;
    public Image image;
    public bool activated;
    public SoulTreeSphere[] requiredSphere;

    private void Start()
    {
        if (!image)
        {
            image = GetComponent<Image>();
        }
    }

    public void SetSphere(bool hehe)
    {
        if (hehe)
        {
            if(!image) image = GetComponent<Image>();
            image.color = Color.white;
        }
    }

    public void ActivateSphere()
    {
        Debug.Log("Activate Sphere");
    }
}
