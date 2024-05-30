using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class space : Sounds
{
    public Transform leftHand;
    public Transform rightHand;
    public Transform rlHand;
    public Transform rrHand;
    public Transform cam;

    public Transform rig;

    private bool _RgrabbingActive = false;
    private bool _LgrabbingActive = false;

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
                PlaySound(0, initialHandPosition, random: false);
                rightController.SendHapticImpulse(defaultAmplitude, defaultDuration);
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
                PlaySound(0, initialHandPosition, random: false);
                leftController.SendHapticImpulse(defaultAmplitude, defaultDuration);
            }


        }
        else if (_LgrabbingActive)
        {
            Vector3 currentHandPosition = lPos.action.ReadValue<Vector3>();
            lhandMovement = currentHandPosition - lastHandPosition;
            Debug.Log(lhandMovement);
            Quaternion final = rig.transform.rotation;
            Vector3 ans = final * lhandMovement;
            rb.AddForce(ans * -1000f, ForceMode.Impulse);
            _LgrabbingActive = false;
            //release event
            lPredPoint.SetActive(true);
            StartCoroutine(ResetCol(rlHand.gameObject));
        }
        else if (_RgrabbingActive)
        {
            Vector3 currentHandPosition = rPos.action.ReadValue<Vector3>();
            rhandMovement = currentHandPosition - lastHandPosition;
            Debug.Log(rhandMovement);
            Quaternion final = rig.transform.rotation;
            Vector3 ans = final * rhandMovement;
            rb.AddForce(ans * -1000f, ForceMode.Impulse);
            _RgrabbingActive = false;
            //release event
            rPredPoint.SetActive(true);
            StartCoroutine(ResetCol(rrHand.gameObject));
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
        //Debug.Log(modify);
        /*if (Mathf.Abs(modify.x) + Mathf.Abs(modify.y) >= 1f && !rotationOn)
        {
            rotationOn = true;
            if(Mathf.Abs(modify.x) >= Mathf.Abs(modify.y))
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
        else if(modify== Vector2.zero && rotationOn)
        {
            rotationOn = false;
        }*/
        Vector2 add = new Vector2(modify.y * -turnspeed * Time.deltaTime, modify.x * -turnspeed * Time.deltaTime);
        var pls = Quaternion.Euler(add);
        rig.transform.rotation = rig.transform.rotation * pls;
    }

    private IEnumerator ResetCol(GameObject go)
    {
        Collider col = go.GetComponent<Collider>();
        col.enabled = false;
        yield return new WaitForSeconds(0.1f);
        col.enabled = true;
    }

    public void ColRes()
    {
        StartCoroutine(ResetCol(rrHand.gameObject));
        StartCoroutine(ResetCol(rlHand.gameObject));

    }
}