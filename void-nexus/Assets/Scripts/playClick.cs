using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playClick : MonoBehaviour
{
    public AudioSource ButtonAudio;
    //public AudioClip Clickk;
    public void playEffect()
    {
        ButtonAudio.Play();
    }
}
