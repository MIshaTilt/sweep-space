using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private float time;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject explosion;

    private void Start()
    {
        health = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        time-= Time.deltaTime;
    }

    private void PushButton()
    {
        rb.AddForce(new Vector3(-100, 0, 0), ForceMode.Impulse);

        if (time < 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(15);
        explosion.SetActive(true);
        health.Die();
    }
}
