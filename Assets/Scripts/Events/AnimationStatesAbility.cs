using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStatesAbility : StateMachineBehaviour
{
    private AnimationEvents events;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(events == null)
        {
            events = animator.GetComponent<AnimationEvents>();
        }

        if (events != null)
        {
            events.AbilityEnd();
        }
    }
}
