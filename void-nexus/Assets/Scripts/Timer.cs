using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Rigidbody rb;
    public AudioSource audioSource;
    public AudioClip track;

    public bool undocked = false;

    private void Start()
    {
        rb.isKinematic = true;
    }

    public void Undock()
    {
        if(slider.value == slider.maxValue && !undocked)
        {   
            rb.isKinematic=false;
            rb.AddForce(new Vector3(0, 0, -1), ForceMode.Impulse);
            //StartCoroutine(end());
            undocked = true;
        }
    }

    private IEnumerator end()
    {
        yield return new WaitForSeconds(2);
        audioSource.clip = track;
        audioSource.Play();
    }
}
