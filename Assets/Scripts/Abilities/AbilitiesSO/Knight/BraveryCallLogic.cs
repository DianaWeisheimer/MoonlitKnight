using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;

[System.Serializable]
public class BraveryCallLogic : AbilityLogic
{
    public float tauntRange = 30;
    public List<Enemy> DetectedEnemies = new List<Enemy>();
    public BraveryCallLogic(Character character) : base(character)
    {

    }

    public override void Activate(float chargeAmount)
    {
        //Taunt
                Debug.Log($"Called Bravery Call!");
        ScanForEnemies();

        foreach (var enemy in DetectedEnemies)
        {
            if (enemy != null && !enemy.dead)
            {
                enemy.character.aggroTable.AddAggro(user, 1000);
                AggroManager.Instance.AddHostileEnemy(enemy);
                Debug.Log($"Taunted {enemy.name} with Bravery Call!");
            }
        }
        //Buff Allies
    }

    private void ScanForEnemies()
    {
        DetectedEnemies.Clear();

        Collider[] hits = Physics.OverlapSphere(user.transform.position, tauntRange);
        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
                DetectedEnemies.Add(enemy);
        }
    }

    public override bool CheckRequirements()
    {
        // Always available; you could add range or stance checks here
        return true;
    }
}
