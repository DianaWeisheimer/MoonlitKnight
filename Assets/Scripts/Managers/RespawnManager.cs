using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameEventsManager.instance.combatEvents.onPlayerDied += HandleDeath;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.combatEvents.onPlayerDied -= HandleDeath;
    }

    private void HandleDeath()
    {
        CharacterStats stats = PartyManager.instance.GetActiveMember().core.character.stats;

        int xp = stats.GetXP();
        stats.SetXP(0);

        DeathDropManager.instance.SpawnDrop(
            PartyManager.instance.GetActiveMember().core.transform.position,
            xp
        );

        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(2f); // death animation / fade

        PartyManager.instance.GetActiveMember().Respawn();

        Transform player = PartyManager.instance.GetActiveMember().core.transform;
        player.position = CheckpointManager.instance.GetRespawnPosition();
        EnemyManager.instance.ResetEnemies();
    }
}

