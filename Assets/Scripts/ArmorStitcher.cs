using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorStitcher : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMesh;
    public SkinnedMeshRenderer playerMesh;
    void Start()
    {
        skinnedMesh.bones = playerMesh.bones;
    }
}
