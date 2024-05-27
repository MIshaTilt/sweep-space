using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : Sounds
{
    [SerializeField] private float health;
    [SerializeField] private TextMeshProUGUI hpDisp;
    private Vector3 lastCheckpoint;



    private bool died;

    // Start is called before the first frame update
    void Start()
    {
        lastCheckpoint = transform.position; // ��������� ������� ������ ��� ������ ����������� �����
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
        Respawn();
    }

    

    public void Respawn()
    {
        transform.position = lastCheckpoint;
        // ������������� ����� �������� ��������, ������������ ������ � �.�.
        health = 100;
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpoint = checkpointPosition;
    }
}
