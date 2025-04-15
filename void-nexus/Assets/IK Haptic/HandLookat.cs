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
    [SerializeField] private bool isHand;
    [SerializeField] private Vector3 rotationOffset;
    [SerializeField] private Transform rig;
    public Transform objectB; // Объект B, к которому будет направлена ось Z объекта A
    public Transform objectC; // Объект C, к которому будет направлена ось Y объекта A

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(isHand)
        {
            if ((!space._LgrabbingActive && left))
            {
                // Направление от объекта A к объекту B (ось Z)
                Vector3 directionToB = (objectB.position - transform.position).normalized;

                // Направление от объекта A к объекту C (ось Y)
                Vector3 directionToC = (objectC.position - transform.position).normalized;

                // Вычисляем перпендикулярное направление для оси X
                Vector3 right = Vector3.Cross(directionToC, directionToB).normalized;

                // Корректируем направление оси Y, чтобы оно было перпендикулярно оси Z и X
                Vector3 correctedUp = Vector3.Cross(directionToB, right).normalized;

                // Создаем поворот на основе направления вперед (к объекту B) и направления вверх (к объекту C)
                transform.rotation = Quaternion.LookRotation(directionToB, correctedUp) * Quaternion.Euler(rotationOffset);
            }
            if ((!space._RgrabbingActive && !left))
            {
                // Направление от объекта A к объекту B (ось Z)
                Vector3 directionToB = (objectB.position - transform.position).normalized;

                // Направление от объекта A к объекту C (ось Y)
                Vector3 directionToC = (objectC.position - transform.position).normalized;

                // Вычисляем перпендикулярное направление для оси X
                Vector3 right = Vector3.Cross(directionToC, directionToB).normalized;

                // Корректируем направление оси Y, чтобы оно было перпендикулярно оси Z и X
                Vector3 correctedUp = Vector3.Cross(directionToB, right).normalized;

                // Создаем поворот на основе направления вперед (к объекту B) и направления вверх (к объекту C)
                transform.rotation = Quaternion.LookRotation(directionToB, correctedUp) * Quaternion.Euler(rotationOffset);
            }
        }
        else
        {
            // Направление от объекта A к объекту B (ось Z)
            Vector3 directionToB = (objectB.position - transform.position).normalized;

            // Направление от объекта A к объекту C (ось Y)
            Vector3 directionToC = (objectC.position - transform.position).normalized;

            // Вычисляем перпендикулярное направление для оси X
            Vector3 right = Vector3.Cross(directionToC, directionToB).normalized;

            // Корректируем направление оси Y, чтобы оно было перпендикулярно оси Z и X
            Vector3 correctedUp = Vector3.Cross(directionToB, right).normalized;

            // Создаем поворот на основе направления вперед (к объекту B) и направления вверх (к объекту C)
            transform.rotation = Quaternion.LookRotation(directionToB, correctedUp) * Quaternion.Euler(rotationOffset);

        }

    }

}
