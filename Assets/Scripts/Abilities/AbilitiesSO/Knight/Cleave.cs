using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "Cleave", menuName = "Scriptable Objects/Ability/Cleave")]
public class Cleave : AbilitySO
{
    public override AbilityLogic CreateLogic(Character abilityHolder)
    {
        return new CleaveLogic(abilityHolder);
    }
    /*public override void Activate(Character abilityHolder)
    {
        if(CheckRequirements(abilityHolder))
        {
            PlayerCharacter playerCharacter = (PlayerCharacter)abilityHolder;
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

        else return false;
    }

    public override void StartCooldown(Character abilityHolder)
    {
        //throw new System.NotImplementedException();
    }*/
}
