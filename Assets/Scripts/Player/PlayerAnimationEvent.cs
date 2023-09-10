using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;

    private void Awake()
    {
        if (!playerAttack) { playerAttack = FindObjectOfType<PlayerAttack>(); }
        if (!playerMovement) { playerMovement = FindObjectOfType<PlayerMovement>(); }
    }

    public void StartAttacking()
    {
        playerAttack.attacking = true;
    }

    public void StopAttacking()
    {
        playerAttack.attacking = false;
    }

    public void StartWeaponCollider()
    {
        playerAttack.StartCollider();
    }

    public void StopWeaponCollider()
    {
        playerAttack.StopCollider();
    }

    public void Shoot()
    {
        playerAttack.Shoot();
    }

    public void StartAimGun()
    {
        playerMovement.shot = false;
    }

    public void StopAimGun()
    {
        playerMovement.shot = true;
    }
}
