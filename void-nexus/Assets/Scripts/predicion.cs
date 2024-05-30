using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class predicion : MonoBehaviour
{
    public Transform Hand;
    public RaycastHit predictionHit;
    public LayerMask grab;
    public LayerMask notGrab;
    public GameObject Sphere;
    public GameObject invChecker;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForSwingPoints();
    }

    private void CheckForSwingPoints()
    {
        RaycastHit sphereCastHit;
        Physics.SphereCast(Hand.position, 0f, Hand.forward, out sphereCastHit, 2f, ~notGrab);

        RaycastHit raycastHit;
        Physics.Raycast(Hand.position, Hand.forward, out raycastHit, 2f, ~notGrab);

        Vector3 realHitPoint;

        if (raycastHit.point != Vector3.zero)
        {
            realHitPoint = raycastHit.point;
        }
        else if (sphereCastHit.point != Vector3.zero)
        {
            realHitPoint = sphereCastHit.point;
        }
        else
        {
            realHitPoint = Vector3.zero;
        }


        if (realHitPoint != Vector3.zero && invChecker.active)
        {
            Sphere.SetActive(true);
            Sphere.transform.position = realHitPoint;
        }
        else
        {
            Sphere.SetActive(false);
        }

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;
    }

}
