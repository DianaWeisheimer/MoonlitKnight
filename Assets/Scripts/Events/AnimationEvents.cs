using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public event Action<bool> Attack;
    public event Action<bool> FreezeMovement;
    public event Action<bool, bool> WeaponCollider;
    public event Action<bool, bool> WeaponTrail;
    public event Action<bool, bool> block;
    public event Action<bool> JumpObstacle;
    public event Action ApplyRootPosition;
    public event Action Footstep;
    public event Action SwitchRightHand;
    public event Action EndAbility;
    public event Action Consume;
    public void FreezeMovementStart()
    {
        FreezeMovement?.Invoke(true);
    }

    public void FreezeMovementFinish()
    {
        FreezeMovement?.Invoke(false);
    }

    public void AttackStart()
    {
        Attack?.Invoke(false);
    }

    public void AttackFinish()
    {
        Attack?.Invoke(true);
    }

    public void RightHandWeaponColliderStart()
    {
        WeaponCollider?.Invoke(true, true);
    }

    public void RightHandWeaponColliderFinish()
    {
        WeaponCollider?.Invoke(false, true);
    }

    public void LeftHandWeaponColliderStart()
    {
        WeaponCollider?.Invoke(true, false);
    }

    public void LeftHandWeaponColliderFinish()
    {
        WeaponCollider?.Invoke(false, false);
    }

    public void LeftHandBlockStart()
    {
        block?.Invoke(false, true);
    }

    public void LeftHandBlockFinish()
    {
        block?.Invoke(false, false);
    }

    public void JumpObstacleStart()
    {
        JumpObstacle?.Invoke(true);
    }

    public void JumpObstacleFinish()
    {
        JumpObstacle?.Invoke(false);
    }

    public void PlayFootstep()
    {
        Footstep?.Invoke();
    }

    public void ApplyRoot()
    {
        ApplyRootPosition?.Invoke();
    }

    public void SwitchHand()
    {
        SwitchRightHand?.Invoke();
    }

    public void AbilityEnd()
    {
        EndAbility?.Invoke();
    }

    public void ConsumeItem()
    {
        Consume?.Invoke();
    }
}
