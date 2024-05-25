using Andtech.ProTracer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : Sounds
{
    [Header("AI settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ImpactEffect;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private Transform gunTip;
    private bool alreadyAttacked = false;

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
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (!alreadyAttacked)
        {
            Shooting();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), cooldown + Random.value);
        }
        if (Vector3.Distance(transform.position, player.transform.position) < 7f)
        {
            gunTip.Rotate(RotationSpeed * Time.deltaTime, 0.0F, 0.0F);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void Shooting()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 7f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 7f))
            {
                Fire();
                var impactEffectIstance = Instantiate(ImpactEffect, transform.position, transform.rotation) as GameObject;
                Destroy(impactEffectIstance, 4);
                PlayerHealth target = hit.transform.GetComponent<PlayerHealth>();
                if (target != null)
                {
                    target.TakeDamage(1f);
                }
                PlaySound(0, transform.position, random: true);
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
