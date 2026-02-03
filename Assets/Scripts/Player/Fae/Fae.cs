using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fae : MonoBehaviour
{
    private bool _absorbable;
    private bool _absorbed;
    public int xpAmmount;
    public Vector3 playerTransform;
    public Transform player;
    public ParticleSystem particle;
    public ParticleSystem trail;
    public ParticleSystem explosion;
    public GameObject orb;
    public AudioSource source;
    public Vector2 sourcePitchRange;
    private void Start()
    {
        StartCoroutine(MakeAbsorbable());
    }

    private IEnumerator MakeAbsorbable()
    {
        yield return new WaitForSeconds(1);
        _absorbable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_absorbed)
        {
            _absorbed = true;
            player = other.transform;
            StartCoroutine(Absorb());
        }
    }

    private IEnumerator Absorb()
    {
        while (_absorbable == false)
        {           
            yield return null;
        }

        //trail.Play();
        float distance = Vector3.Distance(transform.position, player.position);

        while(distance > 0f)
        {
            playerTransform = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, playerTransform, 14 * Time.deltaTime);
            distance = Vector3.Distance(transform.position, playerTransform);
            yield return null;
        }

        //player.GetComponent<Character>().stats.AddXP(xpAmmount);
        GameEventsManager.instance.partyEvents.PartyGainXP(xpAmmount);
        orb.SetActive(false);
        explosion.Play();
        particle.Stop();
        trail.Stop();

        float pitch = Random.Range(sourcePitchRange.x, sourcePitchRange.y);
        source.pitch = pitch;
        source.Play();

        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
