using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Test : MonoBehaviour
{
    public LayerMask playerHitbox;
    public GameObject suka;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        New();
    }

    private void New()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, transform.position, out raycastHit))
        {
            suka.transform.position = raycastHit.transform.position;

        }
    }
}
