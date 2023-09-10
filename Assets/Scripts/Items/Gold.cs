using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public int ammount;
    public bool collected;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !collected)
        {
            GameEventManager.Instance.goldEvents.GoldGained(ammount);
            GameEventManager.Instance.goldEvents.CollectCoin();
            collected = true;
            Destroy(gameObject);
        }
    }
}
