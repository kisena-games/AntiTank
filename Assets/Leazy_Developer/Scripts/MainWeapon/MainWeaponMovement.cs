using UnityEngine;

public class MainWeaponMovement : MonoBehaviour
{
    [Header("Base Moving Parameters")]
    [SerializeField] private Transform _baseTransform;
    [SerializeField] private float _moveBaseSpeed = 10f;

    [Header("Top Moving Parameters")]
    [SerializeField] private Transform _topTransform;

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
        Gizmos.DrawRay(_topTransform.position, _directionWeaponToCursorAim);
    }

    private void Update()
    {
        if (InputManager.IsWeaponBaseMoving)
        {
            RotateBase();
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

        _directionCameraToWeapon = _topTransform.position - Camera.main.transform.position;
        _directionCameraToWorldCursor = resultPosition - Camera.main.transform.position;
        _directionWeaponToCursorAim = resultPosition - _topTransform.position;
    }

    private void RotateBase()
    {
        _baseTransform.Rotate(Vector3.up, InputManager.WeaponBaseMoveInput.x * _moveBaseSpeed * Time.deltaTime);
    }

    private void RotateTop()
    {
        _topTransform.forward = _directionWeaponToCursorAim.normalized;
    }
}
