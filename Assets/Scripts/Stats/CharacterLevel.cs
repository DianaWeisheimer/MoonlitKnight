using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterLevel
{
    public static event Action<CharacterLevel> OnLevelChange;
    private int level;

    public List<SoultTreeSphereInfoSO> soulSpheres;

    public void ApplyBonuses()
    {
        OnLevelChange?.Invoke(this);
    }

    public int GetCharacterLevel()
    {
        level = soulSpheres.Count;

        //for(int i = 0; i < statBonuses.Length; i++)
        //{
            //level += statBonuses[i];
        //}

        return level;
    }
}
