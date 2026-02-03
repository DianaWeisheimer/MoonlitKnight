using UnityEngine;

public class DeathDrop : MonoBehaviour, IInteractable
{
    private int storedXP;

    public string InteractionPrompt => "Collect Fallen Honor";

    public bool IsAvailable => true;

    public void SetXP(int amount)
    {
        storedXP = amount;
    }

    public void Collect(CharacterStats stats)
    {
        stats.AddXP(storedXP);
        Destroy(gameObject);
    }

    public Vector3 GetInteractionPosition()
    {
        return transform.position;
    }

    public void Interact(GameObject interactor)
    {
        Collect(interactor.GetComponent<Player>().character.stats);
    }
}
