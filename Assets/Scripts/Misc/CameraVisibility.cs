using UnityEngine;
using System.Linq;

public class CameraVisibility : MonoBehaviour
{
    public static bool IsVisible(Transform target)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return planes.All(plane => plane.GetDistanceToPoint(target.transform.position) > 0);
    }
}
