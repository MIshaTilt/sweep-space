using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class inventory : MonoBehaviour
{
    public ActionBasedController controller;
    public int selectedWeapon = 0;
    public Animator rightGlove;
    private float gripValue;
    //public GameObject weapons;

    public float defaultAmplitude = 0.2f;
    public float defaultDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        controller.selectAction.action.started += Change;
        //rightGlove = GetComponent<Animator>();
        //DontDestroyOnLoad(controller);
        //DontDestroyOnLoad(weapons);
    }

    // Update is called once per frame
    void Update()
    {
        SelectWeapon();



    }

    private void Change(InputAction.CallbackContext context)
    {
        if (selectedWeapon >= transform.childCount - 1) selectedWeapon = 0;
        else selectedWeapon++;
        switch (selectedWeapon)
        {
            case 1: // ��������
                rightGlove.SetBool("GrabbingPistol", true);
                break;
            case 2: // ����������
                rightGlove.SetBool("GrabbingPistol", false);
                rightGlove.SetBool("GrabbingGun", true);
                break;
            default: // ����� �� ��������� �����
                rightGlove.SetBool("GrabbingGun", false);
                break;


        }
        controller.SendHapticImpulse(defaultAmplitude, defaultDuration);
        /*if (selectedWeapon == 1)
        {
            rightGlove.SetBool("grabbingpistol", true);
        }*/


    }

    //private void AnimateGrip()
    //{
    //    gripValue = controller.selectAction.action.ReadValue<float>();
    //    Debug.Log(gripValue);
    //    rightGlove.SetFloat("Grip", gripValue);
    //    Debug.Log("hand");
    //}

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);



                //if (selectedWeapon == 1)
                //    rightGlove.SetBool("GrabbingPistol", true);
                //else
                //    rightGlove.SetBool("GrabbingPistol", false);

                //if (selectedWeapon == 2)
                //    rightGlove.SetBool("GrabbingGun", true);
                //else
                //    rightGlove.SetBool("GrabbingGun", false);
                //i++;


                if (i == 1 && !rightGlove.GetBool("GrabbingPistol"))
                {
                    rightGlove.SetBool("GrabbingPistol", true);
                }
                //else if (i == 2 && rightGlove.GetBool("GrabbingPistol"))
                //{
                //    rightGlove.SetBool("GrabbingPistol", false);
                //}

                //Debug.Log(weapon);
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
