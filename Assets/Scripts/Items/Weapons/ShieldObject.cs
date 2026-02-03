using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldObject : WeaponObject
{
    public override void Use()
    {
        SetCollider(true);
    }

    public override void CancelUse()
    {
        SetCollider(false);
    }

    public override void SetCollider(bool hehe)
    {
        if (boxCollider)
        {
            boxCollider.enabled = hehe;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack") && blocking)
        {
            AttackStack enemyStack = other.GetComponentInParent<AttackStack>();
            if (enemyStack)
            {
                enemyStack.Blocked();
                character?.equipment.OnBlockSuccess();
            }
        }
    }

}
