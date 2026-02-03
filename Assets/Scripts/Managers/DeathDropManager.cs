using UnityEngine;

public class DeathDropManager : MonoBehaviour
{
    public static DeathDropManager instance;
    public GameObject deathDropPrefab;

    private DeathDrop activeDrop;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnDrop(Vector3 position, int xp)
    {
        if (activeDrop != null)
            Destroy(activeDrop.gameObject);

        GameObject obj = Instantiate(deathDropPrefab, position, Quaternion.identity);
        activeDrop = obj.GetComponent<DeathDrop>();
        activeDrop.SetXP(xp);
    }

    public void ClearDrop()
    {
        activeDrop = null;
    }
}

