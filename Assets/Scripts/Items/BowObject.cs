using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BowObject : ItemObject
{
    public InventoryItem item;
    public Character character;
    public MeshRenderer bladeRenderer;
    public BoxCollider boxCollider;
    public GameObject slash;
    public Transform slashPos;
    public AttackStack attackStack;
    public bool overrideDamage;
    public Vector2 damageOverride;
    public AudioSource swingSource;
    public AudioSource hitSource;
    public Vector2 sourcePitchRange;
    public TrailRenderer trail;

    public Transform stringBone;
    public Transform rightHandPos;
    public Transform firePoint;
    public Transform aimTarget;
    public GameObject arrow;
    public float arrowSpeed;

    public override void Use()
    {
        Shoot();
    }

    public override void CancelUse()
    {
    }

    public override void OnInstantiate(Character character, InventorySlot slot)
    {
    }

    private void Start()
    {
        /*if (!rightHandPos && character.equipment.equipmentPosition != null)
        {
            rightHandPos = character.equipment.equipmentPosition[0];
        }

        aimTarget = character.equipment.aimPosition;*/
    }
    private void Update()
    {
        //if(rightHandPos)stringBone.transform.position = rightHandPos.position;
        //if(aimTarget)firePoint.LookAt(aimTarget);
    }

    public void Shoot()
    {
        if (aimTarget) firePoint.LookAt(aimTarget);

        CalculateDamage();

        GameObject proj = Instantiate(arrow, firePoint.position, firePoint.rotation);

        AttackStack stack = proj.GetComponent<AttackStack>();
        //stack.damage = attackStack.damage;
        stack.attacker = attackStack.attacker;
        stack.bypassIFrames = attackStack.bypassIFrames;

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.transform.forward * arrowSpeed, ForceMode.Impulse);
    }

    public void CalculateDamage()
    {
        WeaponItem weaponItem = item.item as WeaponItem;
        float damage = Random.Range(item.minDamage.maxValue, item.maxDamage.maxValue);

        if (overrideDamage)
        {
            damage = Random.Range(damageOverride.x, damageOverride.y);
        }

        float strengthBonus = damage * (character.stats.GetStat(StatType.Strength).maxValue * weaponItem.scaleStrength / 10000);
        float dexterityBonus = damage * (character.stats.GetStat(StatType.Dexterity).maxValue * weaponItem.scaleDexterity / 10000);
        float intelligenceBonus = damage * (character.stats.GetStat(StatType.Intelligence).maxValue * weaponItem.scaleIntelligence / 10000);
        float wisdomBonus = damage * (character.stats.GetStat(StatType.Wisdom).maxValue * weaponItem.scaleWisdom / 10000);

        //attackStack.damage = damage + strengthBonus + dexterityBonus + intelligenceBonus + wisdomBonus;
    }

}
