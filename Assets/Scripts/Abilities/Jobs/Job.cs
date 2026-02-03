using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewJob", menuName = "Scriptable Objects/New Job")]
public class Job : ScriptableObject
{
    public string jobName;
    public string jobDescription;
    public List<WeaponCategory> rightHandType;
    public List<WeaponCategory> leftHandType;
    public List<ArmorCategory> armorTypes;
    public List<StatBonus> statMultiplier;
    public List<JobAbilityUnlock> abilityUnlock;
    public SoulTree levelingTree;
    public AnimatorOverrideController jobAnimations;
}
