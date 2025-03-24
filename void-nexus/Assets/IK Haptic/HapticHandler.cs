using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticHandler : MonoBehaviour
{
    [SerializeField] private string eventName;
    [SerializeField] private PlayerHealth health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHaptic()
    {
        Debug.Log("esfdcw");
        health.TakeDamage(3f);
    }
}
