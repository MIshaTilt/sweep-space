using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float power;

    private const string WEAPON_TAG = "Weapon";
    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag(WEAPON_TAG))
        {
            return;
        }

        rb.isKinematic = false;
        rb.AddForce(Vector3.up * power);
        Debug.Log("Hello");


    }
}
