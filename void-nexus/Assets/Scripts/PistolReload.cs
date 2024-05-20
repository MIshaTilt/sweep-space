using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR.Input;

public class PistolReload : Sounds
{
    [SerializeField] private FPSFireManager manager;
    [SerializeField] private grenadeLaunceher glmanager;

    private void Start()
    {
        FPSFireManager manager = GetComponent<FPSFireManager>();
        grenadeLaunceher glmanager = GetComponent<grenadeLaunceher>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag("pistol") && !collider.CompareTag("grenadeLauncher"))
        {
            manager.isReloading = false;
            glmanager.isReloading = false;
            return;
        }

        if (collider.CompareTag("pistol") && manager.currentAmmo < manager.maxAmmo)
        {
            manager.isReloading = true;
            PlaySound(0,transform.position, random: false);
        }
        else if(collider.CompareTag("grenadeLauncher") && glmanager.currentAmmo < glmanager.maxAmmo)
        {
            glmanager.isReloading = true;
            PlaySound(0, transform.position, random: false);
        }
        Debug.Log(collider);


    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("pistol")&& manager.currentAmmo!=manager.maxAmmo)
        {
            manager.isReloading = false;
            PlaySound(1, transform.position, random: false);
        }
        else if (collider.CompareTag("grenadeLauncher") && glmanager.currentAmmo != glmanager.maxAmmo)
        {
            glmanager.isReloading = false;
            PlaySound(1, transform.position, random: false);
        }
    }
}
