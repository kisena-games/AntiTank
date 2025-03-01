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
        if (dirX > 0) // ������� ������
        {
            if (_currentDirection == Direction.Forward)
            {
                _currentDirection = Direction.Right;
                _targetRotation = transform.rotation * Quaternion.Euler(0, 90, 0); // ������� �� 90 �������� ������
            }
            else if (_currentDirection == Direction.Right)
            {
                // ������ �� ������, �.�. �� ����� ��������� ������ �� ����������� Right
                return;
            }
            else if (_currentDirection == Direction.Left)
            {
                _currentDirection = Direction.Forward;
                _targetRotation = transform.rotation * Quaternion.Euler(0, 90, 0); // ������ ����� ��������� � Forward
            }
        }
        else if (dirX < 0) // ������� �����
        {
            if (_currentDirection == Direction.Forward)
            {
                _currentDirection = Direction.Left;
                _targetRotation = transform.rotation * Quaternion.Euler(0, -90, 0); // ������� �� 90 �������� �����
            }
            else if (_currentDirection == Direction.Left)
            {
                // ������ �� ������, �.�. �� ����� ��������� ����� �� ����������� Left
                return;
            }
            else if (_currentDirection == Direction.Right)
            {
                _currentDirection = Direction.Forward;
                _targetRotation = transform.rotation * Quaternion.Euler(0, -90, 0); // ������ ����� ��������� � Forward
            }
        }

        _rotationProgress = 0; // ����� ��������� ��������
    }
}
