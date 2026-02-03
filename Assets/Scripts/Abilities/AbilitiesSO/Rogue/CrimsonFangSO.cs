using NUnit.Framework;
using UnityEngine;
using UnityEngine.TextCore.Text;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CrimsonFang", menuName = "Scriptable Objects/Ability/CrimsonFang")]
public class CrimsonFangSO : AbilitySO
{
    public override AbilityLogic CreateLogic(Character abilityHolder)
    {
        return new CrimsonFangLogic(abilityHolder);
    }
}
