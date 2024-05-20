using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reverbEnter : MonoBehaviour
{

    public AudioReverbZone roomZone;
    public GameObject player;
    private void Awake()
    {
        AudioReverbZone roomZone = GetComponent<AudioReverbZone>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            roomZone.enabled = true;
        }
        Debug.Log("on");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            roomZone.enabled = false;
        }
        Debug.Log("off");
    }

}
