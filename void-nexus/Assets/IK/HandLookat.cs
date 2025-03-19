using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HandLookat : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private Vector3 offset;
    [SerializeField] private space space;
    [SerializeField] private bool left;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if ((!space._LgrabbingActive && left))
        {
            Vector3 pos = (head.position + offset) - transform.position;
            transform.rotation = Quaternion.LookRotation(pos);
        }
        if ((!space._RgrabbingActive && !left))
        {
            Vector3 pos = (head.position + offset) - transform.position;
            transform.rotation = Quaternion.LookRotation(pos);
        }

    }
}
