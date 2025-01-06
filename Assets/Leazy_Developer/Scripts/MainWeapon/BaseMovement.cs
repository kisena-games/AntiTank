using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    [Header("Base Moving Parameters")]
    [SerializeField] private Transform _baseTransform;
    [SerializeField] private float _moveBaseSpeed = 50f;

    private void Update()
    {
        if (InputManager.IsWeaponBaseMoving)
        {
            RotateBase();
        }
    }

    private void RotateBase()
    {
        _baseTransform.Rotate(Vector3.up, InputManager.WeaponBaseMoveInput.x * _moveBaseSpeed * Time.deltaTime);
    }
}
