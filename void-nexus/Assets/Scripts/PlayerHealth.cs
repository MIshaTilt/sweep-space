using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Sounds
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private TextMeshProUGUI hpDisp;
    [SerializeField] private space space;
    private Vector3 lastCheckpoint;

    [SerializeField] private Image myNewBar;

    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 1f;
    private float reloadTimer;
    public bool isReloading = false;

    private bool died;

    // Start is called before the first frame update
    void Start()
    {
        lastCheckpoint = transform.position; // Ќачальна€ позици€ игрока как перва€ контрольна€ точка
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            reloadTimer += Time.deltaTime;
            myNewBar.fillAmount = reloadTimer/reloadTime;
            if (reloadTimer > reloadTime)
            {
                isReloading = false;
                myNewBar.fillAmount = 0f;
                health = maxHealth;
                return;
            }
        }
        else
        {
            myNewBar.fillAmount = 0f;
            reloadTimer = 0f;
        }
        hpDisp.text = health.ToString();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        PlaySound(0, transform.position, random: true);
        if (health <= 0f && !died)
        {
            Die();
        }
        
    }

    public void Die()
    {
        PlaySound(0,transform.position, random: false);
        Respawn();
    }

    

    public void Respawn()
    {
        transform.position = lastCheckpoint;
        // ƒополнительно можно сбросить здоровье, перезар€дить оружие и т.д.
        health = 100;
        space.ColRes();
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpoint = checkpointPosition;
    }
}
