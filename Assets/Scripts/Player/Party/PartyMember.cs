using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class PartyMember
{
    public CharacterData characterData;
    public CharacterCore core;
    public CharacterRole role;

    public void ChangeJob(Job job)
    {
        core.character.ChangeJob(job);
    }
    public void Respawn()
    {
        if(role == CharacterRole.Player)
        {
            Player player = core.playerHandler.GetPlayer();
            player.Respawn();
        }
    }
}
