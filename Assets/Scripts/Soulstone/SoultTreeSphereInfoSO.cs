using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoultTreeSphereInfoSO : ScriptableObject
{
    public string sphereName;
    public string sphereDescription;
    [TextArea(10, 100)]
    public string sphereLongDescription;
}
