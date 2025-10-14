using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] private Transform carTransform;
    [Range(1, 10)]
    [SerializeField] private float followSpeed = 2;
    [Range(1, 10)]
	
    [SerializeField] private Vector3 offset = new(0, 0, 0);
	
    [SerializeField] private float lookSpeed = 5;
    [SerializeField] private Vector3 initialCameraPosition;
    [SerializeField] private Vector3 initialCarPosition;
    [SerializeField] private Vector3 absoluteInitCameraPosition;
	
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float orbitAmount = 2f;

    [SerializeField] private float currentOrbit = 0f;

    private Vector3 localOffset;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.08f;

    void Start(){
        if (carTransform == null) { enabled = false; return; }
        localOffset = carTransform.InverseTransformPoint(transform.position);
    }

    void FixedUpdate()
    {
        if (carTransform == null) return;

        var horizontal = 0f;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontal = 10f;
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontal = -10f;
        }

        var targetOrbit = -horizontal * orbitAmount;
        currentOrbit = Mathf.Lerp(currentOrbit, targetOrbit, rotateSpeed * Time.fixedDeltaTime);

        var rotatedLocal = Quaternion.Euler(0f, currentOrbit, 0f) * localOffset;
        var targetPos = carTransform.TransformPoint(rotatedLocal);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

        var targetRot = Quaternion.LookRotation(carTransform.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, lookSpeed * Time.fixedDeltaTime);
    }

}