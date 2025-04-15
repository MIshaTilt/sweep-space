using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    public AudioSource NewSceneMusic; 

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("musicVolume", 1);
        NewSceneMusic.volume = savedVolume;
    }
}

