using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStatesBlock : StateMachineBehaviour
{
    private AnimationEvents events;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(events == null)
        {
            events = animator.GetComponent<AnimationEvents>();
        }

        if (events != null)
        {
            events.LeftHandBlockStart();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (events == null)
        {
            events = animator.GetComponent<AnimationEvents>();
        }

        if (events != null)
        {
            events.LeftHandBlockFinish();
        }
    }
}
