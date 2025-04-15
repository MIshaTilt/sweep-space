using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalButton : MonoBehaviour
{
    public string playerHandTag = "PlayerHand";
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject handImage;
    [SerializeField] private MeshRenderer box;
    [SerializeField] private Image myNewBar;

    private float _holdTimer;
    private bool _isHandInside;
    private bool _notPlayed = true;

    private void Start()
    {
        rb.isKinematic = true;
        explosion.SetActive(false);
        myNewBar.fillAmount = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerHandTag))
        {
            _isHandInside = true;
            _holdTimer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerHandTag))
        {
            _isHandInside = false;
            myNewBar.fillAmount = 0f;
            _holdTimer = 0f;
        }
    }

    private void Update()
    {
        if (_isHandInside && _notPlayed)
        {
            _holdTimer += Time.deltaTime;
            myNewBar.fillAmount = _holdTimer;

            if (_holdTimer >= 1f)
            {
                PerformAction();
                myNewBar.fillAmount = 0f;
                // Для однократного срабатывания раскомментируйте следующую строку:
                // _isHandInside = false;
                _holdTimer = 0f;
            }
        }
    }

    private void PerformAction()
    {
        handImage.SetActive(false);
        box.enabled = false;
        rb.isKinematic = false;
        rb.AddForce(new Vector3(0, 0, -2), ForceMode.Impulse);
        rb.AddTorque(new Vector3(2, -2, -2), ForceMode.Impulse);
        StartCoroutine(explode());
        _notPlayed = false;
        Debug.Log("Действие выполнено!");
        // Здесь разместите свою логику (например, активация объекта, анимация и т.д.)
    }

    private IEnumerator explode()
    {
        yield return new WaitForSeconds(8);
        explosion.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }
}
