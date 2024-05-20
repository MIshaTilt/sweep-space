using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : Sounds
{
    [SerializeField] private float health;
    [SerializeField] private TextMeshProUGUI hpDisp;

    private bool died;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        PlaySound(0, transform.position, random: true);
        if (health <= 0f && !died)
        {
            Die();
        }
        hpDisp.text = health.ToString();
    }

    public void Die()
    {
        PlaySound(0,transform.position, random: false);
    }
}
