using Unity.VisualScripting;
using UnityEngine;

public abstract class AbilitySO : ScriptableObject
{
    public Sprite abilityIcon;
    public string abilityName;
    public string abilityDescription;

    public AnimatorOverrideController animatorOverrideController;

    public float healthCost;
    public float manaCost;
    public float staminaCost;

    public float cooldownTime;
    public float activeTime;

    public bool isChargeable;
    public float maxChargeTime;

    // Runtime logic factory
    public abstract AbilityLogic CreateLogic(Character abilityHolder);
}

