using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class inventoryL : MonoBehaviour
{
    public ActionBasedController controllerL;
    public int selectedItem = 0;
    public Animator leftGlove;

    public float defaultAmplitude = 0.2f;
    public float defaultDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        controllerL.selectAction.action.started += ChangeL;
        //rightGlove = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SelectItem();



    }

    private void ChangeL(InputAction.CallbackContext context)
    {
        if (selectedItem >= transform.childCount - 1) selectedItem = 0;
        else selectedItem++;
        switch (selectedItem)
        {
            case 1: // пистолет
                leftGlove.SetBool("GrabbingBattery", true);
                break;
            case 2: // гранатомет
                leftGlove.SetBool("GrabbingBattery", false);
                leftGlove.SetBool("GrabbingMed", true);
                break;
            default: // сброс до состо€ни€ поко€
                leftGlove.SetBool("GrabbingMed", false);
                break;


                //}
                /*if (selectedWeapon == 1)
                {
                    rightGlove.SetBool("grabbingpistol", true);
                }*/

        }
        controllerL.SendHapticImpulse(defaultAmplitude, defaultDuration);

    }

    //private void AnimateGrip()
    //{
    //    gripValue = controller.selectAction.action.ReadValue<float>();
    //    Debug.Log(gripValue);
    //    rightGlove.SetFloat("Grip", gripValue);
    //    Debug.Log("hand");
    //}

    private void SelectItem()
        {
            int i = 0;
            foreach (Transform weapon in transform)
            {
                if (i == selectedItem)
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


                    if (i == 1 && !leftGlove.GetBool("GrabbingBattery"))
                    {
                        leftGlove.SetBool("GrabbingBattery", true);
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
