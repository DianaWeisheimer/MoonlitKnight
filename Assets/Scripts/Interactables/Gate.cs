using UnityEngine;

public class Gate : MonoBehaviour
{
    public Animator animator;
    public bool IsOpen { get; private set; } = false;
    public bool IsAvailable { get; private set; } = true;

    public void Toggle()
    {
        if (!IsAvailable) return;
        IsAvailable = false;
        IsOpen = !IsOpen;
        animator.SetBool("isOpen", IsOpen);
    }

    // Called by animation event
    public void EnableInteraction()
    {
        IsAvailable = true;
    }
}
