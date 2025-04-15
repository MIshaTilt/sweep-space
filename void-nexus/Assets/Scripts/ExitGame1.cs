using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Timeline.TimelinePlaybackControls;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ExitGame1 : MonoBehaviour
{
    public Timer timer;

    private void OnTriggerEnter(Collider other)
    {
        if (timer.undocked == true)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
