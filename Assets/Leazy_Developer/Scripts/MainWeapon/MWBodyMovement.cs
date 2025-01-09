using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MWBodyMovement : MonoBehaviour
{
    [Header("Body Moving Parameters")]
    [SerializeField] private Transform _body;
    [SerializeField] private float _moveBodySpeed = 50f;

    private void Update()
    {
        if (InputManager.IsWeaponBaseMoving)
        {
            RotateBase();
        }
    }

    private void RotateBase()
    {
        _body.Rotate(Vector3.up, InputManager.WeaponBaseMoveInput.x * _moveBodySpeed * Time.deltaTime);
    }
}
