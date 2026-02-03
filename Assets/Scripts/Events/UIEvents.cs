using System;
using UnityEngine;

public class UIEvents
{
    public event Action<AbilityHolder> onUpdateAbilities;
    public void UpdateAbilities(AbilityHolder abilityHolder)
    {
        if (onUpdateAbilities != null)
        {
            onUpdateAbilities(abilityHolder);
        }
    }

    public event Action<bool, Transform> onSetLockonReticle;
    public void SetLockonReticle(bool lockonReticle, Transform target) 
    {
        onSetLockonReticle?.Invoke(lockonReticle, target);
    }

    public event Action<bool, string> onSetPrompt;
    public void SetPrompt(bool open, string text)
    {
        onSetPrompt?.Invoke(open, text);
    }

    public event Action<Soulstone> onOpenSoulstoneMenu;
    public void OpenSoulstoneMenu(Soulstone soulstone)
    {
        onOpenSoulstoneMenu?.Invoke(soulstone);
    }
}
