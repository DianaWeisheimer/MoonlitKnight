using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string openPrompt = "Open Door";
    public string closePrompt = "Close Door";
    public Animator animator;

    private bool isOpen = false;
    private bool isAvailable = true;

    public string InteractionPrompt => isOpen ? closePrompt : openPrompt;
    public bool IsAvailable => isAvailable;

    public void Interact(GameObject interactor)
    {
        if (!isAvailable) return;

        isAvailable = false;
        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);
    }

    public Vector3 GetInteractionPosition() => transform.position;

    // This gets called from the animation clip via Animation Event
    public void EnableInteraction()
    {
        isAvailable = true;
    }
}
