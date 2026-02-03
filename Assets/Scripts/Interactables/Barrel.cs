using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public Loot loot;
    public GameObject barrelMesh;
    public GameObject barrelDestroyed;
    public bool destroyed;

    /*private void Start()
    {
        if (destroyed)
        {
            barrelMesh.SetActive(false);
            Destroy(barrelMesh);
        }
    }

    public override void Interact(bool hehe, Player player)
    {
        Debug.Log("interacated");
        player.character.animations.animator.SetTrigger("Interact");
    }

    public void DestroyBarrel()
    {
        if(!destroyed)loot.DropLoot();

        interactable = false;
        destroyed = true;
        inRange = false;
        barrelMesh.SetActive(false);
        barrelDestroyed.SetActive(true);
        InspectIcon.SetActive(false);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon") && !destroyed)
        {
            DestroyBarrel();
        }

        else if (other.CompareTag("Player") && !destroyed && interactable)
        {
            inRange = true;
            InspectIcon.SetActive(true);
        }
    }*/
}
