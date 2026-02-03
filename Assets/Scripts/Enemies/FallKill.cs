using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallKill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            other.GetComponent<Character>().stats.Die();
        }
    }
}
