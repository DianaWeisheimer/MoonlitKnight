using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Attack", menuName = "Enemy/Enemy Attack")]
public class EnemyAIAttack : ScriptableObject
{
    public float attackDelay;
    public float attackCooldown;
    public float attackTriggerRange;
    public AnimatorOverrideController attackAnimation;
}
