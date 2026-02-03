using System.Collections.Generic;
using UnityEngine;

public enum CombatSubStateGroupID
{
    Attack,
    Counter,
    Block,
    Movement
}

[System.Serializable]
public class CombatSubStateGroup
{
    public CombatSubStateGroupID id;
    public List<AICombatSubState> states;
}

