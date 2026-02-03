using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class CleaveLogic : AbilityLogic
{
    public CleaveLogic(Character user) : base(user)
    {
        
    }

    public override void Activate(float charge)
    {
        /*if(CheckRequirements(abilityHolder))
        {
            PlayerCharacter playerCharacter = (PlayerCharacter)abilityHolder;
            WeaponItem item = playerCharacter.inventory.rightHand.item as WeaponItem;

            playerCharacter.stats.AbilityCost(this);

            playerCharacter.animations.SetAbilityAnimation(animatorOverrideController);
            playerCharacter.animations.animator.SetTrigger("CastAbility");

            WeaponObject weaponObject = playerCharacter.equipment.rightHandItem as WeaponObject;
            weaponObject.CalculateDamage(0, 2);
        }*/ 
    }

    public override bool CheckRequirements()
    {
        return true;
        /*if (abilityHolder is PlayerCharacter)
        {
            if (abilityHolder.GetComponentInChildren<PlayerMovement>().combatMode && abilityHolder.inventory.rightHand.item != null)
            {
                WeaponItem item = abilityHolder.inventory.rightHand.item as WeaponItem;

                if (item.category == WeaponCategory.Sword)
                {
                    return true;
                }

                else return false;
            }

            else return false;
        }

        else return false;*/
    }
}
