using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MWBodyMovement : MonoBehaviour
{
    public enum Direction { Forward, Left, Right }

    [SerializeField] private float _rotationSpeed = 2.0f;

    private Quaternion _targetRotation;
    private Transform _mwHeadMovement;

    private float _rotationProgress = 0;
    private bool _isRotating = false;

    private void Start()
    {
        _mwHeadMovement = GameObject.FindObjectOfType<MWHeadMovement>().transform;
    }

    private void OnEnable()
    {
        InputManager.BaseRotateAction += RotateBase;
        SwitchCameraMode.SwitchToDefaultModeAction += SwitchToDefault;
    }

    private void OnDisable()
    {
        InputManager.BaseRotateAction -= RotateBase;
        SwitchCameraMode.SwitchToDefaultModeAction -= SwitchToDefault;
    }

    private void Awake()
    {
        _mwHeadMovement = FindObjectOfType<MWHeadMovement>().transform;
    }

    private void Update()
    {
        if (_isRotating)
        {
            if (_rotationProgress < 1.0f)
            {
                _rotationProgress += Time.deltaTime * _rotationSpeed;
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _rotationProgress);
            }
            else
            {
                _isRotating = false;
            }
        }
    }

    private void RotateBase(float dirX)
    {
        if (_isRotating)
        {
            return;
        }

        _targetRotation = transform.rotation * Quaternion.Euler(0, dirX * 90, 0);
        _rotationProgress = 0;
        _isRotating = true;
    }

    private void SwitchToDefault()
    {
        // Calculate the difference between the head's Y rotation and the body's Y rotation
        float angleDifference = Mathf.DeltaAngle(transform.eulerAngles.y, _mwHeadMovement.eulerAngles.y);

        // Check if the angle difference exceeds the threshold.  No reason to constantly set rotation.
        if (Mathf.Abs(angleDifference) > 45)
        {
            // Determine the closest 90-degree multiple
            float currentYRotation = transform.eulerAngles.y; // Current rotation of cannon body
            float closestAngle = Mathf.Round(currentYRotation / 90f) * 90f; // find the cloest cardinal direction

            // Check each angle
            float angle0Diff = Mathf.Abs(Mathf.DeltaAngle(_mwHeadMovement.eulerAngles.y, currentYRotation + 0));
            float angle90Diff = Mathf.Abs(Mathf.DeltaAngle(_mwHeadMovement.eulerAngles.y, currentYRotation + 90));
            float angle180Diff = Mathf.Abs(Mathf.DeltaAngle(_mwHeadMovement.eulerAngles.y, currentYRotation + 180));
            float angle270Diff = Mathf.Abs(Mathf.DeltaAngle(_mwHeadMovement.eulerAngles.y, currentYRotation + 270));
            float minAngle = Mathf.Min(angle0Diff, angle90Diff, angle180Diff, angle270Diff);

            // Determine closest angle
            if (minAngle == angle0Diff) closestAngle = currentYRotation + 0;
            else if (minAngle == angle90Diff) closestAngle = currentYRotation + 90;
            else if (minAngle == angle180Diff) closestAngle = currentYRotation + 180;
            else closestAngle = currentYRotation + 270;

            // Apply the new rotation to the body
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, closestAngle, transform.eulerAngles.z);
        }
    }
}
