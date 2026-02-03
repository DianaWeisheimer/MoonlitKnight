using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class AbilitySensor : MonoBehaviour
{
    public float detectionRadius = 15f;
    public float fovAngle = 120f;
    public Transform eyePoint;
    public LayerMask playerLayer;
    public LayerMask obstructionMask;
    public int raysToCast = 5;
    public List<GameObject> targets;
    private int count;
    Collider[] colliders = new Collider[50];

    public bool PlayerInSight(Transform player)
    {
        Vector3 directionToPlayer = (player.position + Vector3.up * 1.0f) - eyePoint.position;
        float angleToPlayer = Vector3.Angle(Camera.main.transform.forward, directionToPlayer);

        if (directionToPlayer.magnitude <= detectionRadius && angleToPlayer <= fovAngle / 2f)
        {
            // Multi-ray visibility check
            return HasLineOfSight(player);
        }
        return false;
    }

    public List<GameObject> Scan()
    {
        count = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, colliders, playerLayer, QueryTriggerInteraction.Collide);

        targets.Clear();
        for (int i = 0; i < count; ++i)
        {
            GameObject obj = colliders[i].gameObject;

            if (obj.CompareTag("Enemy") && PlayerInSight(obj.transform))
            {
                targets.Add(obj);
            }
        }

        return targets;
    }

    private bool HasLineOfSight(Transform player)
    {
        Vector3[] samplePoints = GetPlayerSamplePoints(player);

        foreach (var point in samplePoints)
        {
            Vector3 dir = (point - eyePoint.position).normalized;
            if (!Physics.Raycast(eyePoint.position, dir, out RaycastHit hit, detectionRadius, obstructionMask))
            {
                // This ray had a clear path to the sampled point on the player
                return true;
            }

            if (hit.transform == player)
                return true;
        }

        return false;
    }

    // Sample various points on the player's body
    private Vector3[] GetPlayerSamplePoints(Transform player)
    {
        Vector3 center = player.position + Vector3.up * 1f;
        return new Vector3[]
        {
            center,
            center + Vector3.up * 0.5f,
            center + Vector3.down * 0.5f,
            center + player.right * 0.5f,
            center - player.right * 0.5f
        };
    }

    private void OnDrawGizmos()
    {
        if (eyePoint == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(eyePoint.position, detectionRadius);

        Vector3 forward = Camera.main.transform.forward;
        Quaternion leftRayRotation = Quaternion.Euler(0, -fovAngle / 2f, 0);
        Quaternion rightRayRotation = Quaternion.Euler(0, fovAngle / 2f, 0);
        Vector3 leftRayDirection = leftRayRotation * forward;
        Vector3 rightRayDirection = rightRayRotation * forward;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(eyePoint.position, leftRayDirection * detectionRadius);
        Gizmos.DrawRay(eyePoint.position, rightRayDirection * detectionRadius);
    }
}
