using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour, IDataPersistence
{
    public static int activeLevel;
    public LevelData levelData;

    public void LoadData(GameData data)
    {
        //activeLevel = data.activeLevel;
    }

    public void SaveData(GameData data)
    {
        //data.activeLevel = activeLevel;
    }
}
