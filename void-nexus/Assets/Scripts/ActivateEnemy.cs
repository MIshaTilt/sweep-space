using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemy : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;

    private void Start()
    {
        foreach (var e in enemies)
        {
            e.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            foreach (var e in enemies)
            {
                e.SetActive(true);
            }
        }
    }
}
