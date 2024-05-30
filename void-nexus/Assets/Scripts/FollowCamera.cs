using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Camera targetCamera; // Камера, перед которой будет располагаться объект
    public float distanceFromCamera = 2f; // Расстояние от камеры до объекта
    public float smoothSpeed = 0.125f; // Скорость плавного движения


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
        // Вычисление позиции объекта перед камерой
        desiredPosition = targetCamera.transform.position + targetCamera.transform.forward * distanceFromCamera;

        // Плавное движение объекта к нужной позиции
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref smoothVelocity, smoothSpeed);

        // Поворот объекта, чтобы он смотрел прямо на камеру
        transform.LookAt(targetCamera.transform);
    }
}
