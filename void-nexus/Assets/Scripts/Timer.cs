using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Rigidbody rb;

    private bool undocked = false;
    
    public void Undock()
    {
        if(slider.value == slider.maxValue && !undocked)
        {
            rb.AddForce(new Vector3(0, 0, -1), ForceMode.Impulse);
            undocked = true;
        }
    }
}
