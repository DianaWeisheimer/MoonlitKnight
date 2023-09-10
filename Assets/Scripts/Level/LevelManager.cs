using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int levelID;
    public Location[] locations;
    public Location currentLocation;
    public bool[] discoveredLocations;
}
