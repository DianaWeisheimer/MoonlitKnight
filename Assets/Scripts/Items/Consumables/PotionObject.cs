using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionObject : ConsumableObject
{
    public override void Consume()
    {
        character.stats.Heal(100);
    }
}
