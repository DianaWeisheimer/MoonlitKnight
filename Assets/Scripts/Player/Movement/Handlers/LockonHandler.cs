using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LockonHandler
{
    public bool lockedTarget;
    public float lockOnRange;
    public Transform lockOnTarget;

    private PlayerMovement movement;
    private Transform player;
    private CinemachineVirtualCamera lockOnCamera;
    private float _nearestDistance;
    private float _distance;

    private LayerMask lockOnLayerMask;

    public List<Transform> validTargets = new List<Transform>();
    public int currentTargetIndex = 0;

    private bool canSwitchTarget;
    private float switchTargetTimer;
    private float refreshTimer;

    public LockonHandler(PlayerMovement _movement ,Transform _player, CinemachineVirtualCamera _lockOnCamera, float _lockonRange, LayerMask _lockonLayerMask)
    {
        movement = _movement;
        player = _player;
        lockOnCamera = _lockOnCamera;
        lockOnRange = _lockonRange;
        lockOnLayerMask = _lockonLayerMask;
    }

    public void Tick()
    {
        if (!lockedTarget)
            return;

        if (Time.time > refreshTimer)
        {
            RefreshValidTargets();
            refreshTimer = Time.time + 0.3f;
        }

        canSwitchTarget = Time.time >= switchTargetTimer + 0.25f;
    }

    public void LockOnTarget()
    {
        if (!lockedTarget)
        {
            FindLockOnTarget();
            return;
        }

        else if (lockedTarget)
        {
            UnlockTarget();
            return;
        }
    }

    public bool FindLockOnTarget()
    {
        validTargets.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(player.position, lockOnRange, lockOnLayerMask);

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Transform target = collider.GetComponentInChildren<CharacterModel>().GetLockOnTarget();
                if (target != null)
                {
                    Vector3 direction = target.transform.position - Camera.main.transform.position;
                    float angle = Vector3.Angle(Camera.main.transform.forward, direction);
                    if (angle < 70f) // only in front
                    {
                        validTargets.Add(target.transform);

                    }
                }
            }
        }

        if(validTargets.Count == 0) 
        { 
            return false; 
        }

        validTargets.Sort((a, b) =>
            Vector3.Distance(player.position, a.position).CompareTo(
            Vector3.Distance(player.position, b.position)));

        lockOnTarget = validTargets[0];
        lockOnCamera.LookAt = validTargets[0];
        movement.animator.SetBool("Strafe", true);
        lockedTarget = true;
        lockOnCamera.gameObject.SetActive(true);
        movement.speed = 5;
        GameEventsManager.instance.uIEvents.SetLockonReticle(true, validTargets[0]);

        return validTargets.Count > 0;
    }

    public void SwitchTargetByMouse(Vector2 mouseDir)
    {
        if (!lockedTarget || !canSwitchTarget)
            return;

        RefreshValidTargets();

        Transform bestTarget = null;
        float bestScore = -Mathf.Infinity;

        Camera cam = Camera.main;

        Vector3 currentScreenPos =
            cam.WorldToScreenPoint(lockOnTarget.position);

        foreach (Transform target in validTargets)
        {
            if (target == lockOnTarget)
                continue;

            Vector3 screenPos =
                cam.WorldToScreenPoint(target.position);

            Vector2 toTarget =
                (Vector2)(screenPos - currentScreenPos);

            // Ignore targets behind camera
            if (toTarget.sqrMagnitude < 0.01f)
                continue;

            toTarget.Normalize();

            float dot = Vector2.Dot(mouseDir, toTarget);

            // Reject targets not in the mouse direction
            if (dot < 0.3f)
                continue;

            // Optional distance bias (closer = better)
            float dist =
                Vector3.Distance(player.position, target.position);

            float score = dot - (dist * 0.01f);

            if (score > bestScore)
            {
                bestScore = score;
                bestTarget = target;
            }
        }

        if (bestTarget == null)
            return;

        lockOnTarget = bestTarget;
        lockOnCamera.LookAt = bestTarget;

        GameEventsManager.instance.uIEvents
            .SetLockonReticle(true, bestTarget);

        switchTargetTimer = Time.time;
    }

    public void SwitchTarget(int direction)
    {
        if (!lockedTarget || !canSwitchTarget)
            return;

        RefreshValidTargets();

        if (validTargets.Count == 0)
        {
            UnlockTarget();
            return;
        }

        currentTargetIndex += direction;
        if (currentTargetIndex < 0)
            currentTargetIndex = validTargets.Count - 1;
        else if (currentTargetIndex >= validTargets.Count)
            currentTargetIndex = 0;

        lockOnTarget = validTargets[currentTargetIndex];
        lockOnCamera.LookAt = lockOnTarget;

        GameEventsManager.instance.uIEvents
            .SetLockonReticle(true, lockOnTarget);

        switchTargetTimer = Time.time;
    }

    private void RefreshValidTargets()
    {
        validTargets.Clear();

        Collider[] hitColliders = Physics.OverlapSphere(
            player.position,
            lockOnRange,
            lockOnLayerMask
        );

        foreach (Collider collider in hitColliders)
        {
            if (!collider.CompareTag("Enemy"))
                continue;

            var model = collider.GetComponentInChildren<CharacterModel>();
            if (model == null) continue;

            Transform target = model.GetLockOnTarget();
            if (target == null) continue;

            Vector3 dir = target.position - Camera.main.transform.position;
            float angle = Vector3.Angle(Camera.main.transform.forward, dir);

            if (angle > 70f)
                continue;

            validTargets.Add(target);
        }

        validTargets.Sort((a, b) =>
            Vector3.Distance(player.position, a.position)
                .CompareTo(Vector3.Distance(player.position, b.position)));
    }

    private bool Blocked(Collider hitCollider)
    {
        RaycastHit hit;
        if (Physics.Linecast(Camera.main.transform.position, hitCollider.transform.position, out hit, lockOnLayerMask))
            //if(!hit.collider.CompareTag("Enemy")) return true;
            if (hit.collider != hitCollider) return true;

        return false;
    }

    public void UnlockTarget()
    {
        GameEventsManager.instance.uIEvents.SetLockonReticle(false, null);
        movement.animator.SetBool("Strafe", false);
        lockedTarget = false;
        lockOnCamera.gameObject.SetActive(false);
        movement.speed = movement.baseSpeed;
    }
}
