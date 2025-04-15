using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Heal : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private InputActionProperty xButton;

    private void OnEnable()
    {
        xButton.action.performed += nachalo;
        xButton.action.canceled += end;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }



    private void nachalo(InputAction.CallbackContext context)
    {
        health.isReloading = true;
        Debug.Log("true");
    }

    private void end(InputAction.CallbackContext context)
    {
        health.isReloading = false;
        Debug.Log("false");
    }
}
