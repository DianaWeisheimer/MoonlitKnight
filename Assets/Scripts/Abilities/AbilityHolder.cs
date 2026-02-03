using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class AbilityHolder
{
    public Character character;
    public List<Ability> ability = new List<Ability>();
    public int abilityIndex;
    public AbilitySensor sensor;

    public void Initialize(Character character)
    {
        this.character = character;
        foreach (var ab in ability)
        {
            ab.Initialize(character);
        }
    }

    public void SwitchAbility()
    {
        abilityIndex++;

        if (abilityIndex > 2)
        {
            abilityIndex = 0;
        }
    }

    public void AbilityPressed()
    {
        if (ability.Count == 0 || abilityIndex >= ability.Count)
            return;

        Ability current = ability[abilityIndex];

        if (current.CheckAvailability(this))
            current.StartCharging();
        /*if (ability.Count == 0 || abilityIndex >= ability.Count)
            return;

        var selected = ability[abilityIndex];

        if (selected.CheckAvailability(this))
        {
            selected.Activate();
            selected.abilityState = AbilityState.active;
            selected.activeTime = Time.time;
            selected.cooldownTime = selected.abilitySO.cooldownTime;
            selected.wasUsed = true;

            // UI/sound/effects...
        }*/
    }

    public void ReleaseAbilityPress()
    {
        if (ability.Count == 0 || abilityIndex >= ability.Count)
            return;

        Ability current = ability[abilityIndex];

        if (current.isCharging)
            current.ReleaseCharge();
    }


    public IEnumerator UpdateAbilityUIRoutine()
    {
        while (true)
        {
            GameEventsManager.instance.uIEvents.UpdateAbilities(this);
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void Update()
    {
        for(int i = 0; i < ability.Count; i++)
        {
            AbilityState previousState = ability[i].abilityState;

            float timeSinceActivated = Time.time - ability[i].activeTime;
            float cooldown = ability[i].abilitySO.cooldownTime;
            bool isCoolingDown = timeSinceActivated < cooldown;
            bool hasEnoughResources = character.stats.CheckAbilityCost(ability[i].abilitySO);

            if (!ability[i].wasUsed)
            {
                ability[i].abilityState = hasEnoughResources ? AbilityState.ready : AbilityState.no_resources;
                ability[i].cooldownTime = 0f;
                continue;
            }

            ability[i].cooldownTime = isCoolingDown ? cooldown - timeSinceActivated : 0f;

            if (isCoolingDown)
            {
                ability[i].abilityState = hasEnoughResources
                    ? AbilityState.cooldown
                    : AbilityState.no_nothing;
            }
            else if (!hasEnoughResources)
            {
                ability[i].abilityState = AbilityState.no_resources;
            }
            else
            {
                ability[i].abilityState = AbilityState.ready;
            }

            if(previousState != ability[i].abilityState)
            {
                GameEventsManager.instance.uIEvents.UpdateAbilities(this);
            }
        }

    }   
}
