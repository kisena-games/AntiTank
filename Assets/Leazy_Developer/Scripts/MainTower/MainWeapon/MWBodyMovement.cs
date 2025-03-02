using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MWBodyMovement : MonoBehaviour
{
    public enum Direction { Forward, Left, Right }

    [SerializeField] private float _rotationSpeed = 2.0f;

    private Quaternion _targetRotation;
    private float _rotationProgress = 0;
    private Direction _currentDirection = Direction.Forward;

    private void OnEnable()
    {
        InputManager.BaseRotateAction += RotateBase;
    }

    private void OnDisable()
    {
        InputManager.BaseRotateAction -= RotateBase;
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
}
