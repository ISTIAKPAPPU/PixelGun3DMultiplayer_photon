using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSensitivity = 3f;
    [SerializeField] private GameObject fpsCamera;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _rotation = Vector3.zero;
    private float _cameraUpAndDownRotation = 0f;
    private float _currentCameraUpAndDownRotation = 0f;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
       
    }

    void Update()
    {
        var xMovement = Input.GetAxis("Horizontal");
        var yMovement = Input.GetAxis("Vertical");

        var horizontalMovement = Vector3.right * xMovement;
        var verticalMovement = Vector3.forward * yMovement;
        var movementVelocity = (horizontalMovement + verticalMovement).normalized * speed;
        Move(movementVelocity);

        // calculate rotation as a 3D vector for turning around
        var yRotation = Input.GetAxis("Mouse X");
        var rotationVector = new Vector3(0, yRotation, 0) * lookSensitivity;
        Rotate(rotationVector);

        //Calculate look up and down camera rotation
        var cameraUpDownRotation = Input.GetAxis("Mouse Y") * lookSensitivity;
        RotateCamera(cameraUpDownRotation);
    }

    private void RotateCamera(float cameraUpDownRotation)
    {
        _cameraUpAndDownRotation = cameraUpDownRotation;
    }

    private void Rotate(Vector3 rotationVector)
    {
        _rotation = rotationVector;
    }

    private void FixedUpdate()
    {
        if (_velocity != Vector3.zero)
        {
            _rb.MovePosition(_rb.position + _velocity * Time.fixedDeltaTime);
        }

        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(_rotation));
        if (fpsCamera != null)
        {
            _currentCameraUpAndDownRotation -= _cameraUpAndDownRotation;
            _currentCameraUpAndDownRotation = Mathf.Clamp(_currentCameraUpAndDownRotation, -80, 80);

            fpsCamera.transform.localEulerAngles = new Vector3(_currentCameraUpAndDownRotation, 0, 0);
        }
    }

    private void Move(Vector3 movementVelocity)
    {
        _velocity = movementVelocity;
    }
}