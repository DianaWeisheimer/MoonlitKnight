using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGrass : MonoBehaviour
{
    public GameObject grass;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            grass.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            grass.SetActive(false);
        }
    }
}
