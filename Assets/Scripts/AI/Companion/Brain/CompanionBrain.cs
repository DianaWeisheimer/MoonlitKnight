using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CompanionBrain : AIBrain
{
    public CompanionRoleProfile role;

    public CompanionStateFollow follow;
    public CompanionStateCombat combat;

    public override void Tick()
    {
        EvaluateThreats();
        //EvaluatePlayerState();
    }

    private void EvaluateThreats()
    {
        List<Enemy> hostileEnemies = AggroManager.Instance.GetAllHostileEnemies();

        if(hostileEnemies.Count > 0)
        {
            ChangeState(combat);
        }
        else
        {
            ChangeState(follow);
        }
    }

    public void StopMovement(bool hehe)
    {
        agent.isStopped = !hehe;
    }

    private void OnEnable()
    {
        character.characterModel.animationEvents.Attack += StopMovement;
    }

    private void OnDisable()
    {
        character.characterModel.animationEvents.Attack -= StopMovement;
    }
}
