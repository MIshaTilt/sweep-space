using Bhaptics.SDK2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHaptic : MonoBehaviour
{
    [SerializeField] private string eventName = "Default";
    private PlayerHealth health;

    [Range(1, 10)]
    public int damage = 3;

    public bool hapticRotate = false;

    // Start is called before the first frame update
    void Start()
    {
        health = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeShot()
    {   
        if(hapticRotate)
        {
            BhapticsLibrary.Play(eventName, 0, 1, 1, 180f, 0);
        }
        else
        {
            BhapticsLibrary.Play(eventName, 0, 1, 1, 0f, 0);
        }
        health.TakeDamage(damage);
    }
}
