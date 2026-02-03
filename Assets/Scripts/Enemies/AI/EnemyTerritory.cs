using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTerritory : MonoBehaviour
{
    public event Action EnteredTerritorry;
    public event Action LeftTerritorry;
    public List<GameObject> intruders;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnteredTerritorry?.Invoke();
            intruders.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LeftTerritorry?.Invoke();
            intruders.Remove(other.gameObject);
        }
    }
}
