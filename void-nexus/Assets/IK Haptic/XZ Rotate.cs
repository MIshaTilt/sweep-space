using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class XZRotate : MonoBehaviour
{
    [SerializeField] private Transform VrRotation;
    [SerializeField] private Transform IkRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {

        IkRotation.transform.rotation = Quaternion.Euler(VrRotation.localEulerAngles.x, IkRotation.transform.rotation.y, VrRotation.localEulerAngles.z);
    }
}
