using UnityEngine;
using System.Collections.Generic;

public class SoulTree : MonoBehaviour
{
    public List<SoulTreeSphere> spheres;

    public void LoadFromCharacter(CharacterLevel character)
    {
        for(int i = 0; i < spheres.Count; i++)
        {
            spheres[i].activated = false;

            for (int j = 0; j< character.soulSpheres.Count; j++)
            {
                if (spheres[i].infoSO == character.soulSpheres[j])
                {
                    spheres[i].activated = true;
                }
            }
        }
    }
}
