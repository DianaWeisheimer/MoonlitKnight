using UnityEngine;

public class PlayerHandler : MonoBehaviour, ICharacterRoleHandler
{
    private CharacterCore core;
    public GameObject playerBrain;
    private Player player;
    public void Initialize(CharacterCore _core)
    {
        core = _core;
        enabled = true;
        GameObject brain = Instantiate(playerBrain, transform);
        brain.transform.parent = transform;

        player = brain.GetComponent<Player>();
        player.Initialize(core.character, brain.GetComponent<PlayerMovement>());
        core.character.type = CharacterType.Player;
        brain.GetComponent<PlayerAttack>().Initialize(brain.GetComponent<Player>(), core.characterModel.animationEvents);
        brain.GetComponent<PlayerMovement>().Initialize(brain.GetComponent<Player>(), core.character, core.controller, core.characterModel.animationEvents, 
        core.characterModel.animator);

        // Hook into input system, camera, etc.
    }
    public Player GetPlayer()
    {
        return player;
    }
    public void Disable()
    {
        enabled = false;
        // Detach camera/input if needed
    }
}

