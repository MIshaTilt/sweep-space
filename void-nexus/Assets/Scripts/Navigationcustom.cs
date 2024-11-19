using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Navigationcustom : MonoBehaviour
{
    [SerializeField] private InputActionProperty yButton;
    [SerializeField] private bool isPressing = false;
    [SerializeField] private Image myBar;
    [SerializeField] private GameObject navigation;
    [SerializeField] private GameObject hint;
    [SerializeField] private GameObject heart;
    [SerializeField] private bool navActive = true;


    public float reloadTime = 1f;
    private float reloadTimer;

    private void OnEnable()
    {
        yButton.action.performed += nachalo;
        yButton.action.canceled += end;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressing)
        {
            reloadTimer += Time.deltaTime;
            myBar.fillAmount = reloadTimer / reloadTime;
            if (reloadTimer > reloadTime)
            {
                isPressing = false;
                myBar.fillAmount = 0f;
                if(navActive)
                {
                    navigation.SetActive(false);
                    hint.SetActive(true);
                    heart.SetActive(true);
                    navActive = false;
                }
                else
                {
                    navigation.SetActive(true);
                    hint.SetActive(false);
                    heart.SetActive(false);
                    navActive = true;
                }

                return;
            }
        }
        else
        {
            myBar.fillAmount = 0f;
            reloadTimer = 0f;
        }
    }

    private void nachalo(InputAction.CallbackContext context)
    {
        isPressing = true;
        Debug.Log("true");
    }

    private void end(InputAction.CallbackContext context)
    {
        isPressing = false;
        Debug.Log("false");
    }
}
