using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private Player player;
    public AnimationEvents animationEvents;

    public void Initialize(Player _player, AnimationEvents _animationEvents)
    {
        player = _player;
        animationEvents = _animationEvents; 
        animationEvents.WeaponCollider += SetWeaponCollider;
    }

    public void SetWeaponCollider(bool hehe, bool rightHand)
    {
        player.character.equipment.SetWeaponCollider(hehe, rightHand);
    }

    public void OnDisable()
    {
        animationEvents.WeaponCollider -= SetWeaponCollider;
    }
}
