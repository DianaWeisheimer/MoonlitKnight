using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public LevelManager levelManager;
    public int locationId;
    public Animator newLocationText;

    private void Start()
    {
        if (!levelManager) { levelManager = FindObjectOfType<LevelManager>(); }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelManager.currentLocation = this;

            if(!levelManager.discoveredLocations[locationId])
            {
                levelManager.discoveredLocations[locationId] = true;
                newLocationText.SetTrigger("Start");
            }
        }
    }
}
