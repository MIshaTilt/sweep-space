using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHaptic : MonoBehaviour
{
    [SerializeField] private string eventName = "Default";
    private HapticHandler hapticHandler;

    // Start is called before the first frame update
    void Start()
    {
        hapticHandler = FindObjectOfType<HapticHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeShot()
    {
        hapticHandler.PlayHaptic(eventName);
        Debug.Log($"Got shot in {eventName}");
    }
}
