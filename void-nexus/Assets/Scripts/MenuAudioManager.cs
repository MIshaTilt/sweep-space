//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class AudioManager : MonoBehaviour
//{
//    public static AudioManager Instance;
//    public AudioSource Theme, Click, MusicVolume;
//    public Slider volumeSlider;

//    private void Start()
//    {
//        if (!PlayerPrefs.HasKey("musicVolume"))
//        {
//            PlayerPrefs.SetFloat("musicVolume", 1);
//            LoadMusic();
//        }
//        else
//        {
//            LoadMusic();
//        }

//    }
//    public void playEffect()
//    {
//        Click.Play();
//    }
//    public void Amplify()
//    {
//        MusicVolume.volume = volumeSlider.value;
//        Save();
//    }

//    public void LoadMusic()
//    {
//        float savedVolume = PlayerPrefs.GetFloat("musicVolume"); 
//        MusicVolume.volume = savedVolume;
//        volumeSlider.value = savedVolume;
//    }

//    public void Save()
//    {
//        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value); 
//        PlayerPrefs.Save();
//    }


//}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource Theme, Click, MusicVolume;
    public Slider volumeSlider;

    /*private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }*/

    private void Start()
    {
        LoadMusic();
    }

    public void playEffect()
    {
        Click.Play();
    }

    public void Amplify()
    {
        MusicVolume.volume = volumeSlider.value;
        Save();
    }

    public void LoadMusic()
    {
        float savedVolume = PlayerPrefs.GetFloat("musicVolume", 1);
        MusicVolume.volume = savedVolume;
        volumeSlider.value = savedVolume;
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        PlayerPrefs.Save();
    }
}
