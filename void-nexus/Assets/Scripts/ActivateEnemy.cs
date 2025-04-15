using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemy : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    
    private void Start()
    {
        foreach (var e in enemies)
        {
            e.SetActive(false);
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            foreach (var e in enemies)
            {
                e.SetActive(true);
            }
        }
    }*/

    public enum TriggerSide { Left, Right, Top, Bottom, Front, Back }
    public TriggerSide triggerSide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 playerPosition = other.gameObject.transform.position;
            Vector3 triggerPosition = transform.position;
            bool isTriggered = false;

            switch (triggerSide)
            {
                case TriggerSide.Left:
                    if (playerPosition.x < triggerPosition.x)
                    {
                        isTriggered = true;
                    }
                    break;
                case TriggerSide.Right:
                    if (playerPosition.x > triggerPosition.x)
                    {
                        isTriggered = true;
                    }
                    break;
                case TriggerSide.Top:
                    if (playerPosition.y > triggerPosition.y)
                    {
                        isTriggered = true;
                    }
                    break;
                case TriggerSide.Bottom:
                    if (playerPosition.y < triggerPosition.y)
                    {
                        isTriggered = true;
                    }
                    break;
                case TriggerSide.Front:
                    if (playerPosition.z > triggerPosition.z)
                    {
                        isTriggered = true;
                    }
                    break;
                case TriggerSide.Back:
                    if (playerPosition.z < triggerPosition.z)
                    {
                        isTriggered = true;
                    }
                    break;
            }
            /*if (playerPosition.x < triggerPosition.x)
            {
                Debug.Log("Left");
            }
            else if (playerPosition.x > triggerPosition.x)
            {
                Debug.Log("Right");
            }
            else if (playerPosition.y > triggerPosition.y)
            {
                Debug.Log("Top");
            }
            else if (playerPosition.y < triggerPosition.y)
            {
                Debug.Log("Bottom");
            }
            else if (playerPosition.z > triggerPosition.z)
            {
                Debug.Log("Front");
            }
            else if (playerPosition.z < triggerPosition.z)
            {
                Debug.Log("Back");
            }*/

            if (isTriggered)
            {
                // Ваш код для обработки срабатывания триггера
                Debug.Log("Триггер сработал!");
                foreach (var e in enemies)
                {
                    e.SetActive(true);
                }
            }
        }
    }
}
