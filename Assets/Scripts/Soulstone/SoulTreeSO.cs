using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SoulTreeSO", menuName = "Scriptable Objects/SoulTreeSO")]
public class SoulTreeSO : ScriptableObject
{
    public List<SoultTreeSphereInfoSO> spheres;
}
