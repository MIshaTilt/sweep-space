using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform objectA;
    public Transform objectB;
    public float X;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Установите позицию объекта B на расстоянии X впереди объекта A
        Vector3 positionB = objectA.transform.position + (objectA.transform.forward * X);
        objectB.transform.position = positionB;

        // Получите текущий угол вращения объекта A по Y-оси
        float rotationY = objectA.transform.eulerAngles.y;

        // Установите угол вращения объекта B по Y-оси, оставив остальные оси без изменений
        objectB.transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}
