using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [SerializeField] private Gate targetGate;
    [SerializeField] private string prompt = "Use Lever";

    public string InteractionPrompt => prompt;
    public bool IsAvailable => targetGate != null && targetGate.IsAvailable;

    public void Interact(GameObject interactor)
    {
        if (targetGate != null)
        {
            targetGate.Toggle();
        }
    }

    public Vector3 GetInteractionPosition() => transform.position;
}
