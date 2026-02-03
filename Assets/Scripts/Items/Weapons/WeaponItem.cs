using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Sword, Bow }

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Item/New Weapon")]
public class WeaponItem : EquipmentItem
{
    public WeaponObject weaponObject;
    public AnimatorOverrideController animations;
    public Vector2 physicalDamage;
    public Vector2 magicDamage;
    public Vector2 fireDamage;
    public Vector2 lightningDamage;
    public Vector2 divineDamage;
    public Vector2 occultDamage;
    public Vector2 trueDamage;
    public float critChance;
    public int poiseDamage;
    public float scaleStrength;
    public float scaleDexterity;
    public float scaleIntelligence;
    public float scaleWisdom;
    public float scaleFaith;
    public WeaponCategory category;
    public List<OnHitEffectSO> critEffects;
}
