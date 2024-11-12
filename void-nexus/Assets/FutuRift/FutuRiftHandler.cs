#region

using ChairControl.ChairWork;
using ChairControl.ChairWork.Options;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

#endregion

namespace Futurift
{
    public class FutuRiftHandler : MonoBehaviour
    {
        private FutuRiftController _futuRiftController;
        public Rigidbody playerRigidbody;

        public float tiltSensitivity = 1.0f;  // Чувствительность наклона
        public float rollSensitivity = 1.0f;  // Чувствительность крена

        public Transform targTrans;

        private Transform lastPosition;         // Предыдущее положение игрока

        private Transform lastTransform;

        private float tilt; // Наклон
        private float roll; // Крен

        public Transform rig;

        void Start()
        {
            // Сохраняем начальную позицию игрока
            lastPosition = targTrans;

            lastTransform = new GameObject().transform;
            lastTransform.position = transform.position;
            lastTransform.rotation = transform.rotation;

        }


        private void Update()
        {
            //Vector3 deltaPosition = transform.position - lastTransform.position;

            // Получаем направление движения
            //Vector3 direction = deltaPosition.normalized;

            // Вычисляем наклон (tilt) относительно оси X (вверх-вниз) и крен (roll) относительно оси Z (влево-вправо)
            //float tilt = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg * tiltSensitivity;
            //float roll = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg * rollSensitivity;

            // Получаем ускорение Rigidbody
            Vector3 acceleration = playerRigidbody.velocity / Time.deltaTime;

            // Преобразуем ускорение в наклон и крен
            // Наклон будет соотноситься с осью X, крен — с осью Z
            //tilt = Mathf.Clamp(acceleration.y, -1f, 1f); // Ограничиваем наклон от -1 до 1
            //roll = Mathf.Clamp(acceleration.x, -1f, 1f); // Ограничиваем крен от -1 до 1

            Vector3 localVelocity = transform.InverseTransformDirection(acceleration);

            Vector3 newAcc = new Vector3(acceleration.x,acceleration.y,acceleration.z);

            Quaternion final = rig.transform.rotation;

            Vector3 ans = final * newAcc;

            tilt = localVelocity.z * tiltSensitivity + transform.rotation.z;
            roll = localVelocity.x * rollSensitivity + transform.rotation.x;

            //var euler = transform.eulerAngles;
            _futuRiftController.Pitch = (tilt > 180 ? tilt - 360 : tilt);
            _futuRiftController.Roll = -(roll > 180 ? roll - 360 : roll);
            Debug.Log($"Pitch: {RoundToDecimalPlaces(_futuRiftController.Pitch, 2)}, Roll: {RoundToDecimalPlaces(_futuRiftController.Roll, 2)},");
        }

        public float RoundToDecimalPlaces(float value, int decimalPlaces)
        {
            float scale = Mathf.Pow(10f, decimalPlaces);
            return Mathf.Round(value * scale) / scale;
        }

        private void OnEnable()
        {
            _futuRiftController = new  FutuRiftController(
                new UdpOptions
                {
                    // ip = "192.168.50.126",  // ip компьютера, на котором запущен контроллер (не локальный)
                    ip= "127.0.0.1",            // докальный
                    port = 6065             // порт, на который настроен контролле
                });

            _futuRiftController.Start();
        }

        private void OnDisable()
        {
            _futuRiftController.Stop();
        }
    }
}
