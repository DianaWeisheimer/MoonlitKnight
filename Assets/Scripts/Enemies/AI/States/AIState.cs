using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    public AIBrain brain;

    private void Awake()
    {
        if (brain == null) { brain = GetComponentInParent<AIBrain>(); }
    }

    public virtual void Tick()
    {

    }

    public virtual void OnStateEnter()
    {

    }

    public virtual void OnStateExit()
    {

    }
}
