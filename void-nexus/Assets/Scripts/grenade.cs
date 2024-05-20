using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : Sounds
{

    public GameObject explosionEffect;
    public float delay = 1f;

    public float explosionForce = 10f;
    public float raduis = 10f;

    Rigidbody rb;
    Target target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("Explode", delay);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, raduis);

        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

            if(rigidbody != null)
            {
                rigidbody.AddExplosionForce(explosionForce, transform.position, raduis, 1f, ForceMode.Impulse);
            }

            Target target = collider.GetComponent<Target>();

            if(target != null)
            {
                target.TakeDamage(100f);
            }
        }
        Instantiate(explosionEffect, transform.position, transform.rotation);
        PlaySound(0, transform.position, random: false, destroyed: true);
        Destroy(gameObject);
    }
}
