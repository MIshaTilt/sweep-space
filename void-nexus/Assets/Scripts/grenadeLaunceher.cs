using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class grenadeLaunceher : Sounds
{
    public Transform spawnPoint;
    public GameObject grenade;
    public InputActionProperty rGrab;

    public float range = 15f;

    private bool shot;

    public TextMeshProUGUI ammo; 
    [SerializeField] private Slider myNewBar;

    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 1f;
    private float reloadTimer;
    public bool isReloading = false;

    public XRBaseController controller;
    public float defaultAmplitude = 0.2f;
    public float defaultDuration = 0.5f;

    private void Start()
    {
        currentAmmo = maxAmmo;
        myNewBar.maxValue = reloadTime;
    }

    private void OnEnable()
    {
        isReloading = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            reloadTimer += Time.deltaTime;
            myNewBar.value = reloadTimer;
            if (reloadTimer > reloadTime)
            {
                isReloading = false;
                myNewBar.value = 0f;
                currentAmmo = maxAmmo;
                return;
            }
        }
        else
        {
            myNewBar.value = 0f;
            reloadTimer = 0f;
        }

        ammo.text = currentAmmo.ToString();

        if (currentAmmo <= 0)
        {
            return;
        }

        if (rGrab.action.ReadValue<float>() >= 0.1f)
        {

            if (!shot)
            {
                Launch();
                currentAmmo--;
                shot = true;
                myNewBar.value = 0;
            }

        }
        else if (shot)
        {
            shot = false;
        }
    }

    private void Launch()
    {
        GameObject grenadeInstance = Instantiate(grenade, spawnPoint.position, spawnPoint.rotation);
        grenadeInstance.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * range, ForceMode.Impulse);
        PlaySound(0, spawnPoint.position, random: true);
        controller.SendHapticImpulse(defaultAmplitude, defaultDuration);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        myNewBar.value = reloadTime;
        isReloading = false;
    }

    public void PublicReload()
    {
        myNewBar.value = 0f;

        StartCoroutine(Reload());
    }
}
