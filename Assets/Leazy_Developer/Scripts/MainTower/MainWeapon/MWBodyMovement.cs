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
        _targetRotation = transform.rotation * Quaternion.Euler(0, dirX * 90, 0);
        _rotationProgress = 0;
    }

    private void SwitchToDefault()
    {
        // реализация поворота тела при переходе из снайперки в обычный режим
    }
}
