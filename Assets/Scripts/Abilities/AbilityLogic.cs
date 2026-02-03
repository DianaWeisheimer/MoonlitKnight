using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public abstract class AbilityLogic
{
    protected Character user;

    public AbilityLogic(Character character)
    {
        this.user = character;
    }

    public abstract void Activate(float chargeAmount);
    public abstract bool CheckRequirements();
}

