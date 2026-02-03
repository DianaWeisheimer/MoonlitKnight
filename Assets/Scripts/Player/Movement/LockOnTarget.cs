using UnityEngine;

[System.Serializable]
public class LockOnTarget
{
    public Transform lockOnPoint; // Where the camera will focus
    public bool IsAlive = true; // You can update this based on enemy health


}