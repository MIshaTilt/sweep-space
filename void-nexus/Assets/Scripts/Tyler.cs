using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tyler: MonoBehaviour 
{
    public AudioSource audioSource;
    public AudioClip track;
    public GameObject skip;
    [SerializeField] private InputActionProperty yButton;

    private void OnEnable()
    {
        yButton.action.performed += Skip;
    }

    // Start is called before the first frame update
    private void Start()
    {
        audioSource.clip = track;
        audioSource.Play();
        skip.SetActive(true);
        StartCoroutine(end());
    }

    void Skip(InputAction.CallbackContext context)
    {
        audioSource.clip = null;
        audioSource.Pause();
        skip.SetActive(false);
    }

    private IEnumerator end()
    {
        yield return new WaitForSeconds(68);
        skip.SetActive(false);
    }
}
