using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soulstone : MonoBehaviour, IInteractable
{
    public int soulstoneID;
    public MeshRenderer crystal;
    public Material[] materials;
    public ParticleSystem[] particles;
    public bool activated;
    public Checkpoint checkpoint;
    [SerializeField] private string inactivePrompt = "Light Soulstone";
    [SerializeField] private string activePrompt = "Rest At Soulstone";

    public string InteractionPrompt => activated ? activePrompt : inactivePrompt;

    public bool IsAvailable => true;

    private void Awake()
    {
        if (!checkpoint)
        {
            checkpoint = GetComponent<Checkpoint>();
        }
    }
    void Start()
    {
        if (activated)
        {
            SetParticles();
        }
    }

    private void SetParticles()
    {
        crystal.material = materials[1];
        particles[0].Stop();
        particles[0].Play();
        particles[1].Stop();
        particles[1].Play();
    }

    public Vector3 GetInteractionPosition()
    {
        return transform.position;
    }

    public void Interact(GameObject interactor)
    {
        if (!activated)
        {
            activated = true;
            SetParticles();
        }

        else
        {
            OpenMenu();
        }
    }

    private void OpenMenu()
    {
        GameEventsManager.instance.uIEvents.OpenSoulstoneMenu(this);
    }
}
