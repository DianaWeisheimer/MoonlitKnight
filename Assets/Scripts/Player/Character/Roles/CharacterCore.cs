using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum CharacterRole { Player, Companion}
public class CharacterCore : MonoBehaviour
{
    public PlayerHandler playerHandler;
    public CompanionHandler companionHandler;
    public CharacterData characterData; // ScriptableObject
    public PlayerCharacter character;
    public CharacterModel characterModel;
    public CharacterController controller;
    public NavMeshAgent agent;
    public CharacterRole currentRole; // Player or Companion

    private ICharacterRoleHandler roleHandler;

    private void Awake()
    {
        //ApplyData(characterData);
    }

    private void Start()
    {
        //SetRole(currentRole);
    }

    public void SetRole(CharacterRole newRole)
    {
        currentRole = newRole;
        ApplyRoleHandler();
    }

    public void ApplyData(CharacterData data)
    {
        //stats = new CharacterStats(data); // health, stamina, etc.
        GameObject hehe = Instantiate(characterData.characterModel, transform);
        characterModel = hehe.GetComponent<CharacterModel>();
        character.Initialize(characterData, characterModel);
        //characterModel.transform.SetParent(transform);
    }

    void ApplyRoleHandler()
    {
        if (roleHandler != null)
            roleHandler.Disable();

        if (currentRole == CharacterRole.Player)
        {
            controller.enabled = true;
            agent.enabled = false;
            roleHandler = gameObject.GetOrAddComponent<PlayerHandler>();
        }

        else if (currentRole == CharacterRole.Companion)
        {
            controller.enabled = false;
            agent.enabled = true;
            roleHandler = gameObject.GetOrAddComponent<CompanionHandler>();
        }

        roleHandler.Initialize(this);
    }
}
