using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class space : Sounds
{
    public Transform leftHand;
    public Transform rightHand;
    public Transform rlHand;
    public Transform rrHand;
    public Transform cam;

    public Transform rig;

    public bool _RgrabbingActive = false;
    public bool _LgrabbingActive = false;

    public InputActionProperty rGrab;
    public InputActionProperty rPos;
    public InputActionProperty lPos;
    public InputActionProperty lGrab;
    public InputActionProperty move;
    public InputActionProperty turn;
    public CapsuleCollider col;
    private Vector3 initialHandPosition;
    private Quaternion initialHandRotation;
    private Vector3 lastHandPosition;
    private Quaternion lastHandRotation;
    private Vector3 rhandMovement;
    private Vector3 lhandMovement;
    public Rigidbody rb;

    public LayerMask grab;
    public LayerMask ignore;

    RaycastHit raycastHit;
    RaycastHit eraycastHit;
    RaycastHit fraycastHit;
    public GameObject lPredPoint;
    public GameObject rPredPoint;

    public GameObject rInvChecker;
    public GameObject lInvChecker;

    public float turnspeed;

    public XRBaseController leftController, rightController;
    public float defaultAmplitude = 0.2f;
    public float defaultDuration = 0.5f;

    public physicsHands rphysics;
    public physicsHands lphysics;

    public bool rotationOn;

    public inventory inventoryR;
    public inventoryL inventoryL;
    public FPSFireManager fpsFireManager;
    public grenadeLaunceher grenadeLaunceher;
    [SerializeField] private TutorialManager tutorialManager;

    void /*Fixed*/Update()
    {
        col.center = cam.transform.localPosition;



        if (rGrab.action.ReadValue<float>() > 0.2f && (lGrab.action.ReadValue<float>() <= 0.2f || lInvChecker.activeSelf) && rInvChecker.activeSelf)
        {
            if (!_RgrabbingActive && !_LgrabbingActive && Physics.SphereCast(rightHand.position, 0f, rightHand.forward, out eraycastHit, 2f, grab))
            {
                _RgrabbingActive = true;
                initialHandPosition = rPos.action.ReadValue<Vector3>();
                initialHandRotation = rrHand.transform.rotation;
                lastHandPosition = initialHandPosition;
                lastHandRotation = initialHandRotation;
                //press event
                rb.velocity = Vector3.zero;
                rPredPoint.SetActive(false);
                Physics.SphereCast(rightHand.position, 0f, rightHand.forward, out raycastHit, 2f, ~ignore);
                Debug.DrawRay(rightHand.position, rightHand.forward);
                //PlaySound(0, cam.position, random: false, destroyed: true);
                rphysics.PlayConnect();
                rightController.SendHapticImpulse(defaultAmplitude, defaultDuration);
                tutorialManager.CompleteStepExternally(8);
            }


        }
        else if (lGrab.action.ReadValue<float>() > 0.2f && (rGrab.action.ReadValue<float>() <= 0.2f || !rInvChecker.activeSelf) && lInvChecker.activeSelf && Physics.SphereCast(leftHand.position, 0f, leftHand.forward, out fraycastHit, 2f))
        {
            if (!_LgrabbingActive && !_RgrabbingActive && Physics.SphereCast(leftHand.position, 0f, leftHand.forward, out fraycastHit, 2f, grab))
            {
                _LgrabbingActive = true;
                initialHandPosition = lPos.action.ReadValue<Vector3>();
                initialHandRotation = rlHand.transform.rotation;
                lastHandPosition = initialHandPosition;
                lastHandRotation = initialHandRotation;
                //press event
                rb.velocity = Vector3.zero;
                lPredPoint.SetActive(false);
                Physics.SphereCast(leftHand.position, 0f, leftHand.forward, out raycastHit, 2f, ~ignore);
                Debug.DrawRay(leftHand.position, leftHand.forward);
                //PlaySound(0, cam.position, random: false, destroyed: true);
                lphysics.PlayConnect();
                leftController.SendHapticImpulse(defaultAmplitude, defaultDuration);
                tutorialManager.CompleteStepExternally(8);
            }


        }
        else if (_LgrabbingActive)
        {
            Vector3 currentHandPosition = lPos.action.ReadValue<Vector3>();
            lhandMovement = currentHandPosition - lastHandPosition;
            //Debug.Log(lhandMovement);
            Quaternion final = rig.transform.rotation;
            Vector3 ans = final * lhandMovement;
            rb.AddForce(ans * -1000f, ForceMode.Impulse);
            _LgrabbingActive = false;
            //release event
            lPredPoint.SetActive(true);
            StartCoroutine(ToggleCollidersCoroutine(rlHand.gameObject));
            if ((ans * -5000f).magnitude > 1f)
            {
                tutorialManager.CompleteStepExternally(10);
            }
        }
        else if (_RgrabbingActive)
        {
            Vector3 currentHandPosition = rPos.action.ReadValue<Vector3>();
            rhandMovement = currentHandPosition - lastHandPosition;
            //Debug.Log(rhandMovement);
            Quaternion final = rig.transform.rotation;
            Vector3 ans = final * rhandMovement;
            rb.AddForce(ans * -1000f, ForceMode.Impulse);
            _RgrabbingActive = false;
            //release event
            rPredPoint.SetActive(true);
            StartCoroutine(ToggleCollidersCoroutine(rrHand.gameObject));
            if((ans * -5000f).magnitude > 1f)
            {
                tutorialManager.CompleteStepExternally(10);
            }
        }

        /*if(Physics.Raycast(cam.position, rPos.action.ReadValue<Vector3>(), out RHit, Vector3.Distance(cam.position, rPos.action.ReadValue<Vector3>())))
        {
            rrHand.position = RHit.point;
        }*/

        // Если кнопка нажата, обновляем позицию камеры
        if (_RgrabbingActive && rGrab.action.ReadValue<float>() > 0.2f)
        {
            Vector3 currentHandPosition = rPos.action.ReadValue<Vector3>();
            rhandMovement = currentHandPosition - lastHandPosition;
            // Двигаем камеру на основе движения руки
            rig.transform.Translate(-rhandMovement);
            //CheckForSwingPoints();
            rrHand.transform.position = raycastHit.point;
            rrHand.transform.rotation = lastHandRotation;
            
            lastHandPosition = currentHandPosition;
            tutorialManager.CompleteStepExternally(9);
            //rb.AddForce(handMovement, ForceMode.Force);
        }
        else if (_LgrabbingActive && lGrab.action.ReadValue<float>() > 0.2f)
        {
            Vector3 currentHandPosition = lPos.action.ReadValue<Vector3>();
            lhandMovement = currentHandPosition - lastHandPosition;
            // Двигаем камеру на основе движения руки
            rig.transform.Translate(-lhandMovement);
            //CheckForSwingPoints();
            rlHand.transform.position = raycastHit.point;
            rlHand.transform.rotation = lastHandRotation;

            lastHandPosition = currentHandPosition;
            tutorialManager.CompleteStepExternally(9);
            //rb.AddForce(handMovement, ForceMode.Force);

        }
        

    }

    private void FixedUpdate()
    {
        if (!_LgrabbingActive && !_RgrabbingActive)
        {
            rb.AddForce(cam.forward * move.action.ReadValue<Vector2>().y + cam.right * move.action.ReadValue<Vector2>().x, ForceMode.Acceleration);
        }

        //Debug.Log(turn.action.ReadValue<Vector2>());
        var modify = turn.action.ReadValue<Vector2>();
        if (PlayerPrefs.GetInt("Movement")==0)
        {
            Vector2 add = new Vector2(modify.y * -turnspeed * Time.deltaTime, modify.x * -turnspeed * Time.deltaTime);
            var pls = Quaternion.Euler(add);
            rig.transform.rotation = rig.transform.rotation * pls;
        }
        else
        {
            if (Mathf.Abs(modify.x) + Mathf.Abs(modify.y) >= 1f && !rotationOn)
            {
                rotationOn = true;
                if (Mathf.Abs(modify.x) >= Mathf.Abs(modify.y))
                {
                    modify.y = 0f;
                }
                else
                {
                    modify.x = 0f;
                }
                Vector2 add = new Vector2(modify.y * -turnspeed, modify.x * -turnspeed);
                var pls = Quaternion.Euler(add);
                rig.transform.rotation = rig.transform.rotation * pls; ;
            }
            else if (modify == Vector2.zero && rotationOn)
            {
                rotationOn = false;
            }
        }
        //Debug.Log(modify);
        
        
    }

    public static IEnumerator ToggleCollidersCoroutine(GameObject target, float delay = 0.1f)
    {
        if (target == null) yield break;

        // Получаем все коллайдеры
        Collider[] allColliders = target.GetComponentsInChildren<Collider>(true);
        List<bool> originalStates = new List<bool>();

        // Сохраняем оригинальные состояния и выключаем
        foreach (Collider col in allColliders)
        {
            originalStates.Add(col.enabled);
            col.enabled = false;
        }

        // Ждем указанное время
        yield return new WaitForSeconds(delay);

        // Восстанавливаем состояния только если объект существует
        if (target != null)
        {
            for (int i = 0; i < allColliders.Length; i++)
            {
                if (allColliders[i] != null)
                {
                    allColliders[i].enabled = originalStates[i];
                }
            }
        }
    }



    public void ColRes()
    {
        StartCoroutine(ToggleCollidersCoroutine(rrHand.gameObject));
        StartCoroutine(ToggleCollidersCoroutine(rlHand.gameObject));
        fpsFireManager.currentAmmo = fpsFireManager.maxAmmo;
        grenadeLaunceher.currentAmmo = grenadeLaunceher.maxAmmo;
    }
}