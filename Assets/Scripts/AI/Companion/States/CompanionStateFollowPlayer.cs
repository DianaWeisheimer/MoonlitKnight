using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionStateFollowPlayer : AIState
{
    public float stoppingDistance;
    public Character player;
    public override void Tick()
    {
        if(player == null)
        {
            player = PartyManager.instance.GetActiveMember().core.character;
            return;
        }
        brain.agent.stoppingDistance = stoppingDistance;
        brain.agent.SetDestination(player.transform.position);
    }
}
