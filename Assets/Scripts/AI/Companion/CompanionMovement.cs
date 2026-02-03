using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionMovement : MonoBehaviour
{
    public Companion companion;
    public bool grounded;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance;
    public bool dashing;
    void Update()
    {
        companion.animator.SetFloat("Speed", companion.agent.velocity.magnitude);

        Gravity();
    }

    private void Gravity()
    {
        grounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        companion.animator.SetBool("Grounded", grounded);
    }

    public void Crouch(bool crouch)
    {
        companion.animator.SetBool("Crouch", crouch);
    }
}
