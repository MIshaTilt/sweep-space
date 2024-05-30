using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Camera targetCamera; // ������, ����� ������� ����� ������������� ������
    public float distanceFromCamera = 2f; // ���������� �� ������ �� �������
    public float smoothSpeed = 0.125f; // �������� �������� ��������


    private Vector3 desiredPosition;
    private Vector3 smoothVelocity = Vector3.zero;

    void Start()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    void Update()
    {
        // ���������� ������� ������� ����� �������
        desiredPosition = targetCamera.transform.position + targetCamera.transform.forward * distanceFromCamera;

        // ������� �������� ������� � ������ �������
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref smoothVelocity, smoothSpeed);

        // ������� �������, ����� �� ������� ����� �� ������
        transform.LookAt(targetCamera.transform);
    }
}
