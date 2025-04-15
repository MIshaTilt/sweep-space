//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class ButtonController : MonoBehaviour
//{
//    public Button settings;
//    private bool isHilighthed = false;
//    private bool isPressed = false;
//    private bool isNormal = true;

//    void Start()
//    {
//        //settings.onClick.AddListener(OnButtonClick);
//    }

//    void Update()
//    {
//        if (isHilighthed)
//        {
//            settings.GetComponent<Animator>().SetTrigger("Highlighthed");
//        }
//        if (isPressed) 
//        {
//            settings.GetComponent<Animator>().SetTrigger("Pressed");
//        }
//        else
//        {
//            settings.GetComponent<Animator>().SetTrigger("Normal");
//        }
//    }

//    //public void OnButtonClick()
//    //{
//    //    isHilighthed = !isHilighthed;
//    //    isNormal = !isNormal;
//    //}

//    public void ResetButton()
//    {
//        isPressed = false;
//        isHilighthed = false;
//        isNormal = true;
//    }

//}

using UnityEngine;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour
{
    public Button button; 
    private Animator animator;
    private bool isHighlighted = false;
    private bool isPressed = false;
    private bool isNormal = true;

    void Start()
    {
        if (button == null)
        {
            Debug.LogError("Button component is not assigned.");
            return;
        }

        animator = button.GetComponent<Animator>(); // Получаем компонент Animator
        if (animator == null)
        {
            Debug.LogError("Animator component is missing on the button.");
            return;
        }

        button.onClick.AddListener(OnButtonClick); // Подписываемся на событие onClick
    }

    void Update()
    {
        // Проверяем, установлен ли Animator
        if (animator != null)
        {
            if (isHighlighted)
            {
                animator.SetTrigger("Highlighted");
            }
            else
            {
                animator.SetTrigger("Normal");
            }
        }
    }

    void OnButtonClick()
    {
        isHighlighted = !isHighlighted;
    }

    public void ResetHighlightState()
    {
        isHighlighted = false;
    }
}
