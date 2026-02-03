using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemArmory : MonoBehaviour
{
    public static ItemArmory instance;

    public List<GameObject> genericItems;
    public List<GameObject> consumables;
    public List<GameObject> keys;
    public List<GameObject> materials;
    public List<GameObject> armors;
    public List<GameObject> swords;
    public List<GameObject> trinkets;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

}
