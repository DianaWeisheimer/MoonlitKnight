using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStates : StateMachineBehaviour
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
            events.FreezeMovementFinish();
        }
    }
}
