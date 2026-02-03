using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    public Character character;
    public CharacterData characterData;
    public List<AIBrainConfiguration> configuration;

    private void Start()
    {
        CreateEnemy();
        EnemyManager.instance.RegisterEnemy(this);
    }

    public void CreateEnemy()
    {
        GameObject obj = Instantiate(characterData.characterModel, transform);
        CharacterModel characterModel = obj.GetComponent<CharacterModel>();
        character.Initialize(characterData, characterModel);

        GameObject brain = Instantiate(characterData.brain.gameObject, transform);
        AIBrain aiBrain = brain.GetComponent<AIBrain>();
        aiBrain.characterModel = characterModel;
        aiBrain.sensor = characterModel.GetComponentInChildren<AISensor>();

        GetComponent<Enemy>().brain = aiBrain;

        if (configuration != null && configuration.Count > 0)
            aiBrain.ConfigureBrain(configuration);

        characterModel.CreateHealthBar(character, HealthBarType.Enemy);
    }

}

