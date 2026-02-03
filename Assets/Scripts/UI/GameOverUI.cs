using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public Animator animator;

    public void ActivateDeathScreen()
    {
        animator.gameObject.SetActive(true);
        animator.SetTrigger("Start");
    }
}
