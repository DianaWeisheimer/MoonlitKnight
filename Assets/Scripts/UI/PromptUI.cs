using TMPro;
using UnityEngine;

public class PromptUI : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI promptText;

    public void SetPrompt(bool open, string text)
    {
        animator.SetBool("Open", open);
        promptText.text = text; 
    }

    private void OnEnable()
    {
        GameEventsManager.instance.uIEvents.onSetPrompt += SetPrompt;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.uIEvents.onSetPrompt -= SetPrompt;
    }
}
