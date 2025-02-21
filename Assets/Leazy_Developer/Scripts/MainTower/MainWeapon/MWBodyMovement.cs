using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MWBodyMovement : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 2.0f;

    private Quaternion _targetRotation;
    private float _rotationProgress = 0;

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

        //if (InputManager.IsWeaponBaseMoving)
        //{
        //    RotateBase();
        //}
    }

    private void RotateBase(float dirX)
    {
        _targetRotation = transform.rotation * Quaternion.Euler(0, dirX * 90, 0);
        _rotationProgress = 0;
    }

    //private void RotateBase()
    //{
    //    Vector2 direction = InputManager.WeaponBaseMoveInput;
    //    transform.forward = new Vector3(direction.x, 0, direction.y);
    //}
}
