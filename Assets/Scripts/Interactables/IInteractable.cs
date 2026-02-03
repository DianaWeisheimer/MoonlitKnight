using UnityEngine;

public interface IInteractable
{
    string InteractionPrompt { get; }
    Vector3 GetInteractionPosition();
    bool IsAvailable { get; }  // ✅ Add this
    void Interact(GameObject interactor);
}
