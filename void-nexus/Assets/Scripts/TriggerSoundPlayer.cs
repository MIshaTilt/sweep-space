using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSoundPlayer : MonoBehaviour
{
    public AudioClip soundToPlay;
    private AudioSource audioTyler;
    private bool hasPlayed = false; 

    private void Start()
    {
        audioTyler = gameObject.AddComponent<AudioSource>();
        audioTyler.clip = soundToPlay;
        audioTyler.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed) 
        {
            audioTyler.Play();
            hasPlayed = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayed = false; 
        }
    }
}
