using NUnit.Framework;
using UnityEngine;
using UnityEngine.TextCore.Text;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BraveryCall", menuName = "Scriptable Objects/Ability/BraveryCall")]
public class BraveryCallSO : AbilitySO
{
    public override AbilityLogic CreateLogic(Character abilityHolder)
    {
        return new BraveryCallLogic(abilityHolder);
    }
}
