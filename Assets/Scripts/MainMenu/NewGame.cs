using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public Animator animator;
    public void NewGamePress()
    {
        StartCoroutine(LoadNewGame());
    }

    public IEnumerator LoadNewGame()
    {
        animator.SetTrigger("NewGameFade");
        yield return new WaitForSeconds(11);
        SceneManager.LoadScene(1);
    }
}
