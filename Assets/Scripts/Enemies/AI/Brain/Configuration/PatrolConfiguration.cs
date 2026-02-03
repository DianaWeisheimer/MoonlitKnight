using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class PatrolConfiguration : AIBrainConfiguration
{
    public List<Transform> scoutAreas;
    public override void ConfigureBrain(AIBrain brain)
    {
        EnemyStatePatrol patrolState = brain.GetComponentInChildren<EnemyStatePatrol>();
        if(patrolState != null) patrolState.patrolPoints = scoutAreas;
    }
}
