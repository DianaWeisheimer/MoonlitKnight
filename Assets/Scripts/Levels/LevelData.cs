using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string levelName;
    public int levelID;
    public LevelData()
    {
        levelName = "Level";
        levelID = -1;
    }
}
