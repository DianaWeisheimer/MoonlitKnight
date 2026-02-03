using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterAnimation
{
    public Animator animator;
    public AnimatorOverrideController defaultController;
    public AnimatorOverrideController activeController;
    private AnimatorOverrideController oldController;

    public void ChangeAnimations(AnimatorOverrideController controller)
    {
        animator.runtimeAnimatorController = controller;
        activeController = controller;
    }

    public void SetAbilityAnimation(AnimatorOverrideController controller)
    {
        oldController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = controller;
    }

    public void ResetAbilityAnimation()
    {
        if(activeController != null)
        {
            animator.runtimeAnimatorController = activeController;
            return;
        }

        else if(oldController != null) 
        {
            animator.runtimeAnimatorController = oldController;
        }
    }

    public void ResetAnimations()
    {
        animator.runtimeAnimatorController = defaultController;
    }
}
