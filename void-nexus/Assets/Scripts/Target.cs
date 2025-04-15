using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Sounds
{
    public float health = 50f;
    public GameObject DestroyEffect;
    public Fly fly;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Material unactive;
    public Animator animator;
    public GameObject noise;
    public GameObject fire;

    public bool died = false;

    private void Start()
    {
        Fly fly = GetComponent<Fly>();
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        Animator animator = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f && !died)
        {
            Die();
        }
    }
    void Die()
    {
        died = true;
        Instantiate(DestroyEffect, transform.position, transform.rotation);
        fly.enabled = false;
        skinnedMeshRenderer.material = unactive;
        PlaySound(0, transform.position, random: true, destroyed: true);
        noise.SetActive(false);
        fire.SetActive(false);
    }

    public bool IsDead()
    {
        return died;
    }
}
