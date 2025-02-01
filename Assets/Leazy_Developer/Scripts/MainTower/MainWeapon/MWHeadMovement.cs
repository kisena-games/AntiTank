using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SwitchCameraMode))]
public class MWHeadMovement : MonoBehaviour
{
    [Header("Head Moving Parameters")]
    [SerializeField] private Transform _head;

    [Header("Sniper Mode Parameters")]
    [SerializeField] private float _mouseSensitivity = 2.0f;
    [SerializeField] private float verticalClampAngle = 10.0f;
    [SerializeField] private float horizontalClampAngle = 45.0f;

    private Vector3 _directionCameraToWeapon = Vector3.zero;
    private Vector3 _directionCameraToWorldCursor = Vector3.zero;
    private Vector3 _directionWeaponToCursorAim = Vector3.zero;

    private Vector2 _rotation = Vector2.zero;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(Camera.main.transform.position, _directionCameraToWeapon);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Camera.main.transform.position, _directionCameraToWorldCursor);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(_head.position, _directionWeaponToCursorAim);
    }

    private void Update()
    {
        Debug.Log(string.Format("_directionWeaponToCursorAim: {0}, _rotation: {1}", _directionWeaponToCursorAim, _rotation));

        if (GamePause.Instance.IsPause)
        {
            return;
        }

        if (InputManager.IsWeaponTopMoving)
        {
            switch (SwitchCameraMode.CurrentMode)
            {
                case CameraMode.Default: DefaultUpdate(); break;
                case CameraMode.Sniper: SniperUpdate(); break;
                default: break;
            }
        }
    }

    private void DefaultUpdate()
    {
        CalculateDirections();
        RotateTop();
    }

    private void SniperUpdate()
    {
        //_rotation = transform.localRotation;
        Vector2 mouseDelta = InputManager.WeaponTopMoveDelta * Time.deltaTime;

        _rotation.y += mouseDelta.x * _mouseSensitivity;
        _rotation.x += mouseDelta.y * _mouseSensitivity;

        _rotation.x = Mathf.Clamp(_rotation.x, -verticalClampAngle, verticalClampAngle);
        _rotation.y = Mathf.Clamp(_rotation.y, -horizontalClampAngle, horizontalClampAngle);

        _head.transform.localRotation = Quaternion.Euler(-_rotation.x, _rotation.y, 0.0f);
        //_head.rotation = Quaternion.Euler(0.0f, _rotation.y, 0.0f);
    }

    private void CalculateDirections()
    {
        Vector3 resultPosition = Vector3.zero;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(InputManager.WeaponTopMoveInput), out RaycastHit hitInfo))
        {
            resultPosition = hitInfo.point;
        }
        else
        {
            resultPosition = Camera.main.transform.position;
        }

        _directionCameraToWeapon = _head.position - Camera.main.transform.position;
        _directionCameraToWorldCursor = resultPosition - Camera.main.transform.position;
        _directionWeaponToCursorAim = resultPosition - _head.position;
    }

    private void RotateTop()
    {
        _head.forward = _directionWeaponToCursorAim.normalized;
    }
}
