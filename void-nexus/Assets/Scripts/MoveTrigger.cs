using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrigger : MonoBehaviour
{
    public enum TriggerSide { Left, Right, Top, Bottom, Front, Back }
    public TriggerSide triggerSideIn;
    public TriggerSide triggerSideOut;
    public bool isTriggeredIn = false;
    public bool isTriggeredOut = false;
    public GameObject[] pool1;
    public GameObject[] pool2;
    public string notDelete = "viewing_deck";
    public string notDelete2 = "start";

    private void Start()
    {
        foreach (var p in pool1)
        {
            if (p.name != notDelete && p.name != notDelete2)
            {
                p.SetActive(false);
            }
        }
        foreach (var p in pool2)
        {
            if (p.name != notDelete && p.name != notDelete2)
            {
                p.SetActive(false);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 playerPosition = other.gameObject.transform.position;
            Vector3 triggerPosition = transform.position;

            switch (triggerSideIn)
            {
                case TriggerSide.Left:
                    if (playerPosition.x < triggerPosition.x)
                    {
                        isTriggeredIn = true;
                    }
                    break;
                case TriggerSide.Right:
                    if (playerPosition.x > triggerPosition.x)
                    {
                        isTriggeredIn = true;
                    }
                    break;
                case TriggerSide.Top:
                    if (playerPosition.y > triggerPosition.y)
                    {
                        isTriggeredIn = true;
                    }
                    break;
                case TriggerSide.Bottom:
                    if (playerPosition.y < triggerPosition.y)
                    {
                        isTriggeredIn = true;
                    }
                    break;
                case TriggerSide.Front:
                    if (playerPosition.z > triggerPosition.z)
                    {
                        isTriggeredIn = true;
                    }
                    break;
                case TriggerSide.Back:
                    if (playerPosition.z < triggerPosition.z)
                    {
                        isTriggeredIn = true;
                    }
                    break;
            }

            switch (triggerSideOut)
            {
                case TriggerSide.Left:
                    if (playerPosition.x < triggerPosition.x)
                    {
                        isTriggeredOut = true;
                    }
                    break;
                case TriggerSide.Right:
                    if (playerPosition.x > triggerPosition.x)
                    {
                        isTriggeredOut = true;
                    }
                    break;
                case TriggerSide.Top:
                    if (playerPosition.y > triggerPosition.y)
                    {
                        isTriggeredOut = true;
                    }
                    break;
                case TriggerSide.Bottom:
                    if (playerPosition.y < triggerPosition.y)
                    {
                        isTriggeredOut = true;
                    }
                    break;
                case TriggerSide.Front:
                    if (playerPosition.z > triggerPosition.z)
                    {
                        isTriggeredOut = true;
                    }
                    break;
                case TriggerSide.Back:
                    if (playerPosition.z < triggerPosition.z)
                    {
                        isTriggeredOut = true;
                    }
                    break;
            }

            if (isTriggeredIn)
            {
                // Ваш код для обработки срабатывания триггера
                Debug.Log("Триггер сработал на вход!");
                if(pool1 != null)
                {
                    foreach (var p in pool1)
                    {
                        p.SetActive(true);
                    }
                }
                if(pool2 != null)
                {
                    foreach (var p in pool2)
                    {
                        p.SetActive(false);
                    }
                }

                
            }
            if (isTriggeredOut)
            {
                // Ваш код для обработки срабатывания триггера
                Debug.Log("Триггер сработал на выход!");
                if(pool2 != null)
                {
                    foreach (var p in pool2)
                    {
                        p.SetActive(true);
                    }
                }
                if(pool1 != null)
                {
                    foreach (var p in pool1)
                    {
                        p.SetActive(false);
                    }
                }


            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isTriggeredIn = false;
        isTriggeredOut = false;
    }
}
