using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[System.Serializable]
public class CrimsonFangLogic : AbilityLogic
{
    public CrimsonFangLogic(Character character) : base(character)
    {

    }

    public override void Activate(float chargeAmount)
    {
        /*// 1. Play animation if needed
        if (data.animatorOverrideController != null)
        {
            user.animator.runtimeAnimatorController = data.animatorOverrideController;
        }

        // 2. Scan for target
        List<GameObject> targets = user.GetComponent<AbilityHolder>()?.sensor?.Scan();

        if (targets == null || targets.Count == 0)
        {
            Debug.Log("No target found.");
            return;
        }

        GameObject target = targets[0]; // take closest target

        // 3. Calculate damage
        float baseDamage = 20f;
        float finalDamage = baseDamage + (chargeAmount * 80f); // 20–100 dmg

        // 4. Apply damage (assume enemy has `Damageable` component)
        if (target.TryGetComponent(out Damageable enemy))
        {
            enemy.TakeDamage(finalDamage);

            // 5. Apply bleed debuff if fully charged
            if (chargeAmount >= 0.99f)
            {
                enemy.ApplyStatusEffect(StatusEffectType.Bleed, 5f); // example
            }

            Debug.Log($"Serpent Fang hit {target.name} for {finalDamage} damage. Charge: {chargeAmount:P0}");
        }*/
    }

    public override bool CheckRequirements()
    {
        // Always available; you could add range or stance checks here
        return true;
    }
}
