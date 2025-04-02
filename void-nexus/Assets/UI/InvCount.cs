using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvCount : MonoBehaviour
{
    [SerializeField] private InputActionProperty change;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject check;
    [SerializeField] private TutorialManager tutorialManager;
    private int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        text.text = ($"{count}/3").ToString();
        check.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCount()
    {
        if (tutorialManager.currentStep == 2)
        {
            if (count < 3)
            {
                count++;
                text.text = ($"{count}/3").ToString();
            }
            if (count == 3)
            {
                check.SetActive(true);
            }
        }
        
    }
}
