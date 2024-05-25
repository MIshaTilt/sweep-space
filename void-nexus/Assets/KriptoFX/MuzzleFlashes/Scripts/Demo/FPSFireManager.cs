using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using static MaterialType;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using Andtech.ProTracer;

public class FPSFireManager : Sounds
{
    public ImpactInfo[] ImpactElemets = new ImpactInfo[0];
    public ImpactInfo ImpactElemet;
    public LayerMask IgnoreMe;
    [Space]
    public float BulletDistance = 100;
    public GameObject ImpactEffect;
    public GameObject DestroyEffect;
    public GameObject stockEffect;
    public InputActionProperty rGrab;
    public TextMeshProUGUI ammo;
    private bool shot = false;

    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 1f;
    private float reloadTimer;
    public bool isReloading = false;
    [SerializeField] private Slider myNewBar;
    public XRBaseController controller;
    public float defaultAmplitude = 0.2f;
    public float defaultDuration = 0.5f;

    public float Speed => 10.0F + (tracerSpeed - 1) * 50.0F;
    public float RotationSpeed => 72.0F;
    [Header("Prefabs")]
    [SerializeField]
    [Tooltip("The Smoke Trail prefab to spawn.")]
    private SmokeTrail smokeTrailPrefab = default;
    [Header("Raycast Settings")]
    [SerializeField]
    [Tooltip("The maximum raycast distance.")]
    private float maxQueryDistance = 300.0F;
    [Header("Tracer Settings")]
    [SerializeField]
    [Tooltip("The speed of the tracer graphics.")]
    [Range(1, 10)]
    private int tracerSpeed = 3;
    [SerializeField]
    [Tooltip("Should tracer graphics use gravity while moving?")]
    private bool useGravity = true;
    [SerializeField]
    [Tooltip("If enabled, a random offset is applied to the spawn point. (This eliminates the \"Wagon-Wheel\" effect)")]
    private bool applyStrobeOffset = true;

    private void Start()
    {
        currentAmmo = maxAmmo;
        myNewBar.maxValue = reloadTime;
    }

    private void OnEnable()
    {
        isReloading = false;
    }

    void Update () {

        if (isReloading)
        {
            reloadTimer += Time.deltaTime;
            myNewBar.value = reloadTimer;
            if (reloadTimer > reloadTime)
            {
                isReloading = false;
                myNewBar.value = 0f;
                currentAmmo = maxAmmo;
                return;
            }
        }
        else 
        {
            myNewBar.value = 0f;
            reloadTimer = 0f;
        }
        

        ammo.text = currentAmmo.ToString();

        if (currentAmmo <= 0)
        {
            return;
        }

	    if (rGrab.action.ReadValue<float>() >= 0.1f && !isReloading) {

            Shot();
           
	    }
        else if (shot)
        {
            shot = false;
        }
    }

    [System.Serializable]
    public class ImpactInfo
    {
        public MaterialType.MaterialTypeEnum MaterialType;
        public GameObject ImpactEffect;
    }

    GameObject GetImpactEffect(GameObject impactedGameObject)
    {
        var materialType = impactedGameObject.GetComponent<MaterialType>();
        if (materialType == null)
            return null;
        foreach (var impactInfo in ImpactElemets)
        {
            if (impactInfo.MaterialType==materialType.TypeOfMaterial)
                return impactInfo.ImpactEffect;
        }
        return null;
    }

    private void Shot()
    {
        if (!shot)
        {
            RaycastHit hit;
            var ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit, BulletDistance, ~IgnoreMe))
            {
                var effect = GetImpactEffect(hit.transform.gameObject);
                if (effect == null)
                    effect = stockEffect;
                var effectIstance = Instantiate(effect, hit.point, new Quaternion()) as GameObject;
                effectIstance.transform.LookAt(hit.point + hit.normal);
                Fire();
                Destroy(effectIstance, 20);

                var impactEffectIstance = Instantiate(ImpactEffect, transform.position, transform.rotation) as GameObject;

                Destroy(impactEffectIstance, 4);
                shot = true;
                Debug.Log("shot");
                Rigidbody rigidbody = hit.transform.gameObject.GetComponent<Rigidbody>();

                if (rigidbody != null)
                {
                    rigidbody.AddExplosionForce(5f, transform.position, 100f, 1f, ForceMode.Impulse);
                }
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    Target target = hit.transform.GetComponent<Target>();
                    target.TakeDamage(50f);
                }
                controller.SendHapticImpulse(defaultAmplitude, defaultDuration);
                currentAmmo--;
                myNewBar.value -= 1 / maxAmmo;
                PlaySound(0,transform.position, random: true);
            }

        }
    }
    public void Fire()
    {
        // Compute tracer parameters
        float speed = Speed;
        float offset;
        if (applyStrobeOffset)
        {
            offset = Random.Range(0.0F, CalculateStroboscopicOffset(speed));
        }
        else
        {
            offset = 0.0F;
        }

        // Instantiate the tracer graphics
        SmokeTrail smokeTrail = Instantiate(smokeTrailPrefab);

        // Setup callbacks
        smokeTrail.Completed += OnCompleted;

        // Use different tracer drawing methods depending on the raycast
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, maxQueryDistance))
        {
            smokeTrail.DrawLine(transform.position, hitInfo.point, speed, offset);

        }
        else
        {
            // Since we have no end point, use DrawRay
            smokeTrail.DrawRay(transform.position, transform.forward, speed, 25.0F, offset);
        }
    }

    private void OnCompleted(object sender, System.EventArgs e)
    {
        // Handle complete event here
        if (sender is TracerObject tracerObject)
        {
            Destroy(tracerObject.gameObject);
        }
    }

    private float CalculateStroboscopicOffset(float speed) => speed * Time.smoothDeltaTime;
}
