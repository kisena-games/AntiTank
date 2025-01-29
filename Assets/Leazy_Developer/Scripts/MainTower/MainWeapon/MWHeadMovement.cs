using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MWHeadMovement : MonoBehaviour
{
    [Header("Head Moving Parameters")]
    [SerializeField] private Transform _head;

    private Vector3 _directionCameraToWeapon = Vector3.zero;
    private Vector3 _directionCameraToWorldCursor = Vector3.zero;
    private Vector3 _directionWeaponToCursorAim = Vector3.zero;

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
        if (GamePause.Instance.IsPause)
        {
            return;
        }

        CalculateDirections();
        if (InputManager.IsWeaponTopMoving)
        {
            RotateTop();
        }
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
