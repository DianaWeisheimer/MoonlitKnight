using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityState { ready, cooldown, no_resources, no_nothing, active }

[System.Serializable]
public class Ability
{
    public AbilitySO abilitySO;
    public AbilityState abilityState;
    public float cooldownTime;
    public float activeTime;
    public bool wasUsed = false;

    public bool isCharging = false;
    public float chargeStartTime = 0f;

    private AbilityLogic logic;

    public void Initialize(Character holder)
    {
        if (abilitySO != null)
        {
            logic = abilitySO.CreateLogic(holder);
        }
        abilityState = AbilityState.ready;
    }

    public void StartCharging()
    {
        if (abilitySO.isChargeable)
        {
            isCharging = true;
            chargeStartTime = Time.time;
        }
        else
        {
            Activate(0f); // Treat as instant if not chargeable
        }
    }

    public void ReleaseCharge()
    {
        if (!isCharging) return;

        isCharging = false;
        float chargeDuration = Time.time - chargeStartTime;
        float chargePercent = Mathf.Clamp01(chargeDuration / abilitySO.maxChargeTime);

        Activate(chargePercent);
    }

    public bool CheckAvailability(AbilityHolder holder)
    {
        return abilityState == AbilityState.ready
            && holder.character.stats.CheckAbilityCost(abilitySO)
            && logic.CheckRequirements();
    }

    private void Activate(float chargeAmount)
    {
        logic.Activate(chargeAmount); // Pass charge info
        abilityState = AbilityState.active;
        activeTime = Time.time;
        cooldownTime = abilitySO.cooldownTime;
        wasUsed = true;
    }
}

