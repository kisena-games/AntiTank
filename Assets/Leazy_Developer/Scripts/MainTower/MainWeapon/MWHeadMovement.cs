using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SwitchCameraMode))]
public class MWHeadMovement : MonoBehaviour
{
    [Header("Sniper Mode Parameters")]
    [SerializeField] private float _mouseSensitivity = 2.0f;
    [SerializeField] private float verticalClampAngle = 10.0f;
    [SerializeField] private float horizontalClampAngle = 100.0f;

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
        Gizmos.DrawRay(transform.position, _directionWeaponToCursorAim);
    }

    private void Update()
    {
        if (GamePause.Instance.IsPause)
        {
            return;
        }

        if (InputManager.IsWeaponTopMoving)
        {
            CalculateDirections();
            CalculateRotation();

            switch (SwitchCameraMode.CurrentMode)
            {
                case CameraMode.Default: DefaultRotateHead(); break;
                case CameraMode.Sniper: SniperRotateHead(); break;
                default: break;
            }
        }
    }

    private void DefaultRotateHead()
    {
        transform.forward = _directionWeaponToCursorAim.normalized;
        ClampVertical();
    }

    private void SniperRotateHead()
    {
        // Применяем вращение
        transform.Rotate(-_rotation.x, _rotation.y, 0.0f);

        ClampVertical();
        ClampHorizontal();

    }
    private void ClampVertical()
    {
        float currentXAngle = transform.eulerAngles.x;
        // Ограничиваем вертикальный угол поворота
        currentXAngle = transform.eulerAngles.x;
        if (currentXAngle > 180) currentXAngle -= 360; // Приводим угол к диапазону -180..+180

        // Ограничиваем вертикальный угол
        currentXAngle = Mathf.Clamp(currentXAngle, -verticalClampAngle, verticalClampAngle);
        transform.eulerAngles = new Vector3(currentXAngle, transform.eulerAngles.y, 0);
    }
    private void ClampHorizontal()
    {
        float currentYAngle = transform.eulerAngles.y;
        // Ограничиваем горизонтальный угол
        currentYAngle = transform.eulerAngles.y;
        if (currentYAngle > 180) currentYAngle -= 360; // Приводим угол к диапазону -180..+180

        currentYAngle = Mathf.Clamp(currentYAngle, -horizontalClampAngle, horizontalClampAngle);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentYAngle, 0);
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

        _directionCameraToWeapon = transform.position - Camera.main.transform.position;
        _directionCameraToWorldCursor = resultPosition - Camera.main.transform.position;
        _directionWeaponToCursorAim = resultPosition - transform.position;
    }

    private void CalculateRotation()
    {
        Vector2 mouseDelta = InputManager.WeaponTopMoveDelta * Time.deltaTime;

        _rotation.y = mouseDelta.x * _mouseSensitivity;
        _rotation.x = mouseDelta.y * _mouseSensitivity;
    }
}
