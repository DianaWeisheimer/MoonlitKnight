using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableEffect", menuName = "Scriptable Objects/ConsumableEffects/Heal Effect")]
public class ConsumableEffectHeal : ConsumableEffect
{
    public float healingAmmount;
    public override void Consume(Character character)
    {
        Debug.Log("healsd");
        character.stats.Heal(healingAmmount);
    }
}
