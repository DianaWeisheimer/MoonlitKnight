using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Player player;
    public Animator animator;
    public bool attacking;
    public bool attackable;
    public Weapon weapon;
    public int comboIndex;
    public float attackDelay;

    void Start()
    {
        comboIndex = 0;
        attacking = false;
        attackable = true;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouse1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (!attacking && attackable)
        {
            player.DrainStamina(-1);

            animator.SetTrigger("Attack" + comboIndex.ToString());
            //animator.SetTrigger("Attack0");
            weapon.OnAttack();

            StartCoroutine(AttackDelay());
            AttackCombo();
        }
    }  

    public IEnumerator AttackDelay()
    {
        float delay = 0.5f - (player.stats.dexterity * 0.009f);
        attackable = false;
        yield return new WaitForSeconds(delay);
        attackable = true;
    }

    public void AttackCombo()
    {
        if(comboIndex <= 2)
        {
            comboIndex++;
        }

        else
        {
            comboIndex = 0;
        }
    }

    public void StartCollider()
    {
        if (weapon)
        {
            weapon.StartCollider();
        }
    }

    public void StopCollider()
    {
        if (weapon)
        {
            weapon.StopCollider();
        }
    }

    public void Shoot()
    {
        if (weapon)
        {
            weapon.Shoot();
        }
    }
}
