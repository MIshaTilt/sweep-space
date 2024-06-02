//using UnityEngine;

//public class FollowCamera : MonoBehaviour
//{
//    public Camera targetCamera; // ������, ����� ������� ����� ������������� ������
//    public float distanceFromCamera = 2f; // ���������� �� ������ �� �������
//    public float smoothSpeed = 0.125f; // �������� �������� ��������


//    private Vector3 desiredPosition;
//    private Vector3 smoothVelocity = Vector3.zero;

//    void Start()
//    {
//        if (targetCamera == null)
//        {
//            targetCamera = Camera.main;
//        }
//    }

//    void Update()
//    {
//        // ���������� ������� ������� ����� �������
//        desiredPosition = targetCamera.transform.position + targetCamera.transform.forward * distanceFromCamera;

//        // ������� �������� ������� � ������ �������
//        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref smoothVelocity, smoothSpeed);

//        // ������� �������, ����� �� ������� ����� �� ������
//        transform.LookAt(targetCamera.transform);
//    }
//}


//using UnityEngine;

////[RequireComponent(typeof(Camera))]
//public class CameraRig : MonoBehaviour
//{

//    public GameObject menuObject; 
//    public Camera playerCamera; 
//    private Vector3 lastHeadPosition;
//    public float smoothSpeed = 0.125f;


//    private Vector3 smoothVelocity = Vector3.zero;


//    void Start()
//    {
//        //playerCamera = GetComponent<Camera>();
//        if (menuObject == null)
//        {
//            Debug.LogError("Menu object is not assigned.");
//        }
//    }

//    void Update()
//    {

//        Vector3 headPosition = playerCamera.transform.position + playerCamera.transform.forward * +0.8f; 


//        if (headPosition != lastHeadPosition)
//        {
//            //menuObject.transform.position = headPosition;
//            menuObject.transform.position = Vector3.SmoothDamp(transform.position, headPosition, ref smoothVelocity, smoothSpeed);
//            menuObject.transform.LookAt(playerCamera.transform.position); 
//        }

//        lastHeadPosition = headPosition;
//    }
//}


//using UnityEngine;


//public class CameraRig : MonoBehaviour
//{
//    public GameObject menuObject; 
//    public Camera playerCamera; 
//    private Vector3 lastHeadPosition; 

//    void Start()
//    {
//        //playerCamera = GetComponent<Camera>();
//        if (menuObject == null)
//        {
//            Debug.LogError("Menu object is not assigned.");
//        }
//    }

//    void Update()
//    {
//        // �������� ������� ������� ������ ������
//        Vector3 headPosition = playerCamera.transform.position + playerCamera.transform.forward * +0.9f; 

        
//        if (headPosition != lastHeadPosition)
//        {
//            menuObject.transform.position = headPosition;
//            menuObject.transform.LookAt(playerCamera.transform.position); 

//        lastHeadPosition = headPosition;
//    }
//}
    



