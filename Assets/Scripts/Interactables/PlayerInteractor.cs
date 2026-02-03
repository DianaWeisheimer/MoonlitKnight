using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public float interactionRange = 3f;
    public LayerMask interactionLayer;
    public IInteractable currentInteractable;

    void Update()
    {
        ScanForInteractables();

        if (currentInteractable != null)
        {
            ShowPrompt(currentInteractable.InteractionPrompt);

            if (Input.GetKeyDown(KeyCode.E))
            {
                currentInteractable.Interact(gameObject);
            }
        }
        else
        {
            HidePrompt();
        }
    }

    void ScanForInteractables()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactionRange, interactionLayer);
        float closest = float.MaxValue;
        IInteractable nearest = null;

        foreach (var hit in hits)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();
            if (interactable != null && interactable.IsAvailable) // ✅ Respect availability
            {
                float dist = Vector3.Distance(transform.position, interactable.GetInteractionPosition());
                if (dist < closest)
                {
                    closest = dist;
                    nearest = interactable;
                }
            }
        }

        if (currentInteractable != nearest)
        {
            HidePrompt();
            currentInteractable = nearest;

            if (currentInteractable != null)
                ShowPrompt(currentInteractable.InteractionPrompt);
        }
    }


    void ShowPrompt(string promptText)
    {
        // Hook into your UI system
        //Debug.Log($"[E] {promptText}");
        GameEventsManager.instance.uIEvents.SetPrompt(true, $"Press [E] To {promptText}");
    }

    void HidePrompt()
    {
        GameEventsManager.instance.uIEvents.SetPrompt(false, string.Empty);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
