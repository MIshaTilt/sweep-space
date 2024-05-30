using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
//using static UnityEditor.FilePathAttribute;



public class Fly : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float rotationalDamp = .5f;
    [SerializeField] float detectionDistance = 20f;
    [SerializeField] float rayCastOffset = 2.5f;
    [SerializeField] float thrust = 1000f;

    private float timer = 0f;
    private Vector3[] directions = new Vector3[6];
    private int currentDirectionIndex = 0;
    private float rotationTimer = 0f;
    private bool isChangingDirection = false;

    public Rigidbody rb;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
        Pathfinding();
    }

    void Turn()
    {
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
    }

    void Move()
    {
        
            Vector3 targetLocation = target.position - transform.position;
            float distance = targetLocation.magnitude;
            rb.AddRelativeForce(Vector3.forward * Mathf.Clamp((distance - 2) / 50, 0f, 1f) * thrust);
    }


    void Pathfinding()
    {
        RaycastHit hit;
        Vector3 raycastOffset = Vector3.zero;

        Vector3 left = transform.position - transform.right * rayCastOffset;
        Vector3 right = transform.position + transform.right * rayCastOffset;
        Vector3 up = transform.position + transform.up * rayCastOffset;
        Vector3 down = transform.position - transform.up * rayCastOffset;

        Debug.DrawRay(left, transform.forward, Color.cyan);
        Debug.DrawRay(right, transform.forward, Color.cyan);
        Debug.DrawRay(up, transform.forward, Color.cyan);
        Debug.DrawRay(down, transform.forward, Color.cyan);


        if (Physics.Raycast(left, transform.forward, out hit, detectionDistance)) raycastOffset += Vector3.right;
        else if (Physics.Raycast(right, transform.forward, out hit, detectionDistance))
            raycastOffset -= Vector3.right;

        if (Physics.Raycast(up, transform.forward, out hit, detectionDistance))
            raycastOffset -= Vector3.up;
        else if (Physics.Raycast(down, transform.forward, out hit, detectionDistance))
            raycastOffset += Vector3.up;

        if (raycastOffset != Vector3.zero)
            transform.Rotate(raycastOffset * 10f * Time.deltaTime);
        else
            Turn();
    }

}
