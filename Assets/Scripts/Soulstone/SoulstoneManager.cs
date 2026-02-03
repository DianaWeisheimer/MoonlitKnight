using System;
using System.Collections.Generic;
using UnityEngine;

public class SoulstoneManager : MonoBehaviour
{
    public static SoulstoneManager instance;
    public Soulstone[] soulstones;
    public int activeSoulstone;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        soulstones = FindObjectsByType<Soulstone>(FindObjectsSortMode.None);
    }
}
