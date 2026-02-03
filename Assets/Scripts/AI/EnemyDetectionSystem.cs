using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionSystem : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 15f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField]private Transform player;
    public static EnemyDetectionSystem Instance { get; private set; }
    public List<Enemy> DetectedEnemies = new List<Enemy>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (detectionRadius <= 0)
        {
            Debug.LogWarning("Detection radius is set to zero or negative. Adjusting to default value of 15.");
            detectionRadius = 50f;
        }
    }

    private void Update()
    {
        if (!player) 
        { 
            GetPlayerPosition(); 
            return; 
        }

        ScanForEnemies();
    }

    private void ScanForEnemies()
    {
        DetectedEnemies.Clear();

        Collider[] hits = Physics.OverlapSphere(player.position, detectionRadius, enemyLayer);
        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
                DetectedEnemies.Add(enemy);
        }
    }

    public List<Enemy> GetHostileEnemies()
    {
        List<Enemy> hostiles = new List<Enemy>();
        foreach (var enemy in DetectedEnemies)
        {
            if (AggroManager.Instance.IsEnemyHostile(enemy))
                hostiles.Add(enemy);
        }
        return hostiles;
    }

    private void GetPlayerPosition()
    {
        PartyMember member = PartyManager.instance.GetActiveMember();
        if (member.core) { player = member.core.transform; }
        else { Debug.LogWarning("No active party member found. Player position cannot be updated."); }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
