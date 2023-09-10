using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public GameObject bullet;
    public Transform firepoint;
    public float bulletSpeed;
    public ParticleSystem shootParticle;
    public AudioSource source;

    public override void Shoot()
    {
        ShootBullet();
    }

    public void ShootBullet()
    {
        if (shootParticle)
        {
            shootParticle.Stop();
            shootParticle.Play();
        }
        
        GameObject proj = Instantiate(bullet, firepoint.position, firepoint.rotation);
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        rb.AddForce(firepoint.transform.forward * bulletSpeed, ForceMode.Impulse);
        if (source) { source.Play(); }
    }
}
