using UnityEngine;

public class GravityHandler
{
    public bool grounded;
    public bool freezeGravity;
    public Vector3 velocity;
    public float gravity;

    private float groundDistance;
    private Transform groundCheck;
    private Transform slopeCheck;

    private LayerMask groundMask;

    private CharacterController controller;
    private Animator animator;
    public GravityHandler(float _gravity, CharacterController _characterController, Transform _groundCheck, float _groundDistance, 
        Transform _slopeCheck, LayerMask _groundMask, Animator _animator)
    {
        gravity = _gravity;
        controller = _characterController;
        groundCheck = _groundCheck;
        groundDistance = _groundDistance;
        slopeCheck = _slopeCheck;
        groundMask = _groundMask;
        animator = _animator;
    }

    public void Tick()
    {
        Gravity();
        //SlopeSliding();
    }

    public void Gravity()
    {
        if (freezeGravity == false)
        {
            grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            animator.SetBool("Grounded", grounded);

            if (grounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            controller.Move(velocity * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
        }
    }

    private void SlopeSliding()
    {
        if (!grounded)
        {
            if (Physics.SphereCast(slopeCheck.position, controller.radius - 0.05f, Vector3.down,
                out var hit, 1f, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore))
            {
                var collider = hit.collider;
                var angle = Vector3.Angle(Vector3.up, hit.normal);

                //Debug.DrawLine(hit.point, hit.point + hit.normal, Color.black, 5f);
                //Gizmos.DrawWireSphere(castOrigin + Vector3.down, movement.controller.radius - 0.1f);
                if (angle > controller.slopeLimit)
                {
                    var normal = hit.normal;
                    var yInverse = 1f - normal.y;
                    velocity.x += yInverse * normal.x;
                    velocity.z += yInverse * normal.z;
                }
            }
        }
    }
}
