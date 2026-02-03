using UnityEngine;

[CreateAssetMenu(fileName = "BleedOnHitEffectSO", menuName = "OnHitEffects/BleedOnHitEffectSO")]
public class BleedOnHitEffectSO : OnHitEffectSO
{
    public override OnHitEffect CreateOnHitEffect()
    {
        return new BleedOnHitEffect();
    }
}
