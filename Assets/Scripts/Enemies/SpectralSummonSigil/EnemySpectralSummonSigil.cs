using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpectralSummonSigil : MonoBehaviour
{
    public Transform[] spawnPositions;
    public GameObject spectralWarrior;
    public GameObject player;
    public int currentWave;
    public int waves;
    public int waveCooldown;

    void Start()
    {
        StartFight();
    }

    public void StartFight()
    {
        StopAllCoroutines();
        
        if(currentWave <= waves)
        {
            SpawnEnemies();
            StartCoroutine(SpawnCooldown());
        }

        currentWave++;
    }

    private void SpawnEnemies()
    {
        for(int i = 0; i< spawnPositions.Length; i++)
        {
            GameObject enemy = Instantiate(spectralWarrior, spawnPositions[i].position, spawnPositions[i].rotation);
            //EnemyBrainSpectralSummonWarrior brain = enemy.GetComponent<Enemy>().enemyAI as EnemyBrainSpectralSummonWarrior;
            //brain.player = player;
            //brain.ChangeState(brain.states[1]);
        }
    }

    private IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(waveCooldown);
        StartFight();
    }
}
