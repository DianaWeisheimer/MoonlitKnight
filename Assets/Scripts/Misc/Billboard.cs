using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    void Start()
    {
        cam = Camera.main.transform;
    }

    /*void Update()
    {
        transform.LookAt(cam);
        transform.Rotate(0, 180, 0);
    }*/

    void LateUpdate()
    {
        Vector3 lookDirection = cam.position - transform.position;
        lookDirection.Normalize();

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), 10 * Time.deltaTime);
        
    }
}
