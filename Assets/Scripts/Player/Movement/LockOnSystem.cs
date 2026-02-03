using System.Collections.Generic;
using UnityEngine;

public class LockOnSystem : MonoBehaviour
{
    public float lockOnRange = 20f;
    public LayerMask targetLayer;
    public Transform playerCamera;
    public Transform player;
    public Transform currentTarget;
    public KeyCode lockOnKey = KeyCode.Tab;

    private List<Transform> validTargets = new List<Transform>();
    private int currentTargetIndex = 0;

    void Update()
    {
        /*if (Input.GetKeyDown(lockOnKey))
        {
            if (currentTarget != null)
            {
                UnlockTarget();
            }
            else
            {
                FindTargets();
                if (validTargets.Count > 0)
                {
                    LockOnToTarget(validTargets[0]);
                }
            }
        }

        if (currentTarget != null)
        {
            RotatePlayerTowardTarget();

            // Optional: Target switching (e.g., left/right arrow keys)
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                SwitchTarget(-1);
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                SwitchTarget(1);
        }*/
    }

    public void LockOnPressed()
    {
        if (currentTarget != null)
        {
            UnlockTarget();
        }
        else
        {
            FindTargets();
            if (validTargets.Count > 0)
            {
                LockOnToTarget(validTargets[0]);
            }
        }
    }

    void FindTargets()
    {
        validTargets.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, lockOnRange, targetLayer);
        foreach (Collider collider in hitColliders)
        {
            LockOnTarget target = collider.GetComponent<LockOnTarget>();
            if (target != null && target.IsAlive)
            {
                Vector3 direction = collider.transform.position - playerCamera.position;
                float angle = Vector3.Angle(playerCamera.forward, direction);
                if (angle < 60f) // only in front
                {
                    validTargets.Add(collider.transform);
                }
            }
        }

        validTargets.Sort((a, b) =>
            Vector3.Distance(player.position, a.position).CompareTo(
            Vector3.Distance(player.position, b.position)));
    }

    void LockOnToTarget(Transform target)
    {
        currentTarget = target;
        currentTargetIndex = validTargets.IndexOf(target);
    }

    void UnlockTarget()
    {
        currentTarget = null;
    }

    void RotatePlayerTowardTarget()
    {
        if (currentTarget == null) return;

        Vector3 direction = currentTarget.position - player.position;
        direction.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        player.rotation = Quaternion.Slerp(player.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void SwitchTarget(int direction)
    {
        if (validTargets.Count <= 1) return;

        currentTargetIndex += direction;
        if (currentTargetIndex < 0) currentTargetIndex = validTargets.Count - 1;
        else if (currentTargetIndex >= validTargets.Count) currentTargetIndex = 0;

        LockOnToTarget(validTargets[currentTargetIndex]);
    }
}
