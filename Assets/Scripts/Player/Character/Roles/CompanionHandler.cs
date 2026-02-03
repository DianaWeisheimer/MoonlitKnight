using UnityEngine;

public class CompanionHandler : MonoBehaviour, ICharacterRoleHandler
{
    public CharacterCore core;
    public GameObject companionBrain;

    public void Initialize(CharacterCore _core)
    {
        core = _core;
        enabled = true;

        core.character.type = CharacterType.Companion;
        GameObject brain = Instantiate(companionBrain, transform);
        brain.GetComponent<Companion>().Initialize(core.character, core.characterModel.animator, core.characterModel.animationEvents, core.agent);
        // Set up follow target, combat AI, etc.
    }

    public void Disable()
    {
        enabled = false;
    }
}

