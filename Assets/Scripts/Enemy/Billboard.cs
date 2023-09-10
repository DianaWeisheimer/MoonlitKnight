using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    void Start()
    {
        //cam = Camera.main;
    }


    void Update()
    {
        transform.LookAt(cam.transform);

        Quaternion.Euler(0f, - transform.rotation.eulerAngles.y, 0f);
    }
}
