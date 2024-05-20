using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public GameObject explosionEffect;
    public float delay = 1f;

    public float explosionForce = 10f;
    public float raduis = 10f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("Explode", delay);
    }

    private void Explode()
    {
        Destroy(gameObject);
    }
}
