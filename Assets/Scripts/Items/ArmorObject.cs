using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorObject : MonoBehaviour
{
    public ArmorItem armorItem;
    public BaseStat baseStats;
    public Stats stats;

    private void Start()
    {
        //stats.SetBaseStats(baseStats);
    }
}
