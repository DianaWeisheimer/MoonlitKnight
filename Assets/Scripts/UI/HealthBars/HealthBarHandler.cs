using UnityEngine;

public enum HealthBarType
{
    Enemy,
    Ally,
    Neutral
}

[System.Serializable]
public class HealthBarHandler
{
    public Transform healthBarPos;
    private EnemyHealthBar currentHealthBar;

    private GameObject GetHealthBar(HealthBarType healthBarType)
    {
        GameObject healthBarPrefab = null;

        switch (healthBarType)
        {
            case HealthBarType.Enemy:
                healthBarPrefab = Resources.Load<GameObject>("EnemyHealthBar");
                break;
            case HealthBarType.Ally:
                healthBarPrefab = Resources.Load<GameObject>("AllyHealthBar");
                break;
            case HealthBarType.Neutral:
                healthBarPrefab = Resources.Load<GameObject>("NeutralHealthBar");
                break;
        }

        return healthBarPrefab;
    }

    public void CreateHealthBar(Character character, HealthBarType healthBarType)
    {
        if (currentHealthBar != null) return;

        GameObject healthBarPrefab = GetHealthBar(healthBarType);
        if (healthBarPrefab != null)
        {
            GameObject healthBarInstance = GameObject.Instantiate(healthBarPrefab, healthBarPos.position, Quaternion.identity, healthBarPos);
            currentHealthBar = healthBarInstance.GetComponent<EnemyHealthBar>();
            currentHealthBar.character = character;
        }
    }

    public void UpdateHealthBar(Character character)
    {
        
    }
}
