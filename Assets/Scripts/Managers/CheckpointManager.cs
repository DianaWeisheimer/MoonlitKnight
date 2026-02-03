using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    private Checkpoint currentCheckpoint;

    private void Awake()
    {
        instance = this;
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public Vector3 GetRespawnPosition()
    {
        if(currentCheckpoint == null) { return new Vector3(0,0,0); }

        return currentCheckpoint.respawnPoint.position;
    }
}

