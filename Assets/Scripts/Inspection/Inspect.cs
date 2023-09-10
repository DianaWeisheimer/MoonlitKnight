using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inspect : MonoBehaviour
{
    public bool inRange;
    public GameObject icon;
    public GameObject inspectCam;
    public GameObject inspectText;

    void Start()
    {
        CheckRange(inRange);
        inspectCam.SetActive(false);
        inspectText.SetActive(false);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().inspects.Add(this);
            CheckRange(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().inspects.Remove(this);
            CheckRange(false);
        }
    }

    public void CheckRange(bool hehe)
    {
        inRange = hehe;      
        icon.SetActive(hehe);
    }

    public void StartInspect(bool hehe)
    {
        inspectCam.SetActive(hehe);
        inspectText.SetActive(hehe);
    }
}
