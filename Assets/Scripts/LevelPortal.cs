using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortal : MonoBehaviour
{
    public GameObject portalCam;
    public string levelName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EnterPortal());
        }
    }
    
    public IEnumerator EnterPortal()
    {
        portalCam.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelName);
    }
}
