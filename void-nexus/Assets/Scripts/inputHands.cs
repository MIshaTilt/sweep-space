using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputHands : MonoBehaviour
{
    public InputActionProperty pinchAction;
    public InputActionProperty gripAction;

    private Animator _animator = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float triggerValue = pinchAction.action.ReadValue<float>();
        _animator.SetFloat("Trigger", triggerValue);

        float gripValue = gripAction.action.ReadValue<float>();
        _animator.SetFloat("Grip", gripValue);
    }

}
