using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Undock : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float timer;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private GameObject explosion;

    private bool check = false;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value == slider.maxValue)
        {
            rb.AddForce(new Vector3(0,-1,0), ForceMode.Impulse);
            if(Time.time > timer && !check)
            {
                check = true;
                StartCoroutine(death());
            }
        }
    }

    private IEnumerator death()
    {
        yield return new WaitForSeconds(5f);
        explosion.SetActive(true);
        yield return new WaitForSeconds(.5f);
        health.TakeDamage(100f);

    }
}
