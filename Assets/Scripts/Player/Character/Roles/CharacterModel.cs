using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    public Animator animator;
    public AnimationEvents animationEvents;
    public List<Transform> handPos;

    [SerializeField] private LockOnTarget lockOnTarget;
    [SerializeField] private HealthBarHandler healthBarHandler;

    public Transform GetLockOnTarget()
    {
        if(lockOnTarget.lockOnPoint != null) return lockOnTarget.lockOnPoint;
        else return this.transform;
    }

    public void CreateHealthBar(Character character, HealthBarType type)
    {
        healthBarHandler.CreateHealthBar(character, type);
    }
}
