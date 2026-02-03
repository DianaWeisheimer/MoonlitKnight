using NUnit.Framework;
using UnityEngine;
using UnityEngine.TextCore.Text;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ThrowingKnives", menuName = "Scriptable Objects/Ability/ThrowingKnives")]
public class ThrowingKnvies : AbilitySO
{
    public GameObject dagger;
    public override AbilityLogic CreateLogic(Character abilityHolder)
    {
        return new ThrowingKnivesLogic(abilityHolder, dagger);
    }
    /*public override void Activate(Character abilityHolder)
    {
        if (CheckRequirements(abilityHolder))
        {
            Debug.Log("Throwing Knives Activated");

            //Get Enemy targets
            

            //Play Animation


            //Instantiate daggers


            //ThrowDaggers



            /*PlayerCharacter playerCharacter = (PlayerCharacter)abilityHolder;
            WeaponItem item = playerCharacter.inventory.rightHand.item as WeaponItem;

            playerCharacter.stats.AbilityCost(this);

            playerCharacter.animations.SetAbilityAnimation(animatorOverrideController);
            playerCharacter.animations.animator.SetTrigger("CastAbility");

            WeaponObject weaponObject = playerCharacter.equipment.rightHandItem as WeaponObject;
            weaponObject.CalculateDamage(0, 2);
        }
    }

    public override bool CheckRequirements(Character abilityHolder)
    {
        if (abilityHolder is PlayerCharacter)
        {
            if (abilityHolder.GetComponentInChildren<PlayerMovement>().combatMode)
            {
                return true;
            }

            return true;
        }

        else return false;
    }

    public override void StartCooldown(Character abilityHolder)
    {
        //throw new System.NotImplementedException();
    }*/
}
