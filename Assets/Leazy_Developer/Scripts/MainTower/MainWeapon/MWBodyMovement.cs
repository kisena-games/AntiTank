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
    private float _rotationProgress = 0;

    private Transform _mwHeadMovement;
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
        if (_rotationProgress < 1.0f)
        {
            _rotationProgress += Time.deltaTime * _rotationSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _rotationProgress);
        }
    }

    private void RotateBase(float dirX)
    {
        float currentYAngle = transform.eulerAngles.y;
        float nearestMultiple = Mathf.Round(currentYAngle / 90) * 90;
        _targetRotation = Quaternion.Euler(0, nearestMultiple + dirX * 90, 0);
        _rotationProgress = 0;
    }

    private void SwitchToDefault()
    {
        float angleDifference = Mathf.DeltaAngle(transform.eulerAngles.y, _mwHeadMovement.eulerAngles.y);

        if (Mathf.Abs(angleDifference) > 90)
        {
            float currentYRotation = transform.eulerAngles.y;
            float closestAngle = Mathf.Round(currentYRotation / 90f) * 90f;

            float angle0Diff = Mathf.Abs(Mathf.DeltaAngle(_mwHeadMovement.eulerAngles.y, currentYRotation + 0));
            float angle90Diff = Mathf.Abs(Mathf.DeltaAngle(_mwHeadMovement.eulerAngles.y, currentYRotation + 90));
            float angle180Diff = Mathf.Abs(Mathf.DeltaAngle(_mwHeadMovement.eulerAngles.y, currentYRotation + 180));
            float angle270Diff = Mathf.Abs(Mathf.DeltaAngle(_mwHeadMovement.eulerAngles.y, currentYRotation + 270));
            float minAngle = Mathf.Min(angle0Diff, angle90Diff, angle180Diff, angle270Diff);

            if (minAngle == angle0Diff) closestAngle = currentYRotation + 0;
            else if (minAngle == angle90Diff) closestAngle = currentYRotation + 90;
            else if (minAngle == angle180Diff) closestAngle = currentYRotation + 180;
            else closestAngle = currentYRotation + 270;

            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, closestAngle, transform.eulerAngles.z);
        }
    }
}
