using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionCrouchTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Companion"))
        {
            other.GetComponent<Companion>().movement.Crouch(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Companion"))
        {
            other.GetComponent<Companion>().movement.Crouch(false);
        }
    }
}
