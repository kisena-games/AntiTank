using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MWBodyMovement : MonoBehaviour
{
    [Header("Body Moving Parameters")]
    [SerializeField] private Transform _body;

    private void Update()
    {
        if (GamePause.Instance.IsPause)
        {
            return;
        }

        if (InputManager.IsWeaponBaseMoving)
        {
            RotateBase();
        }
    }

    private void RotateBase()
    {
        Vector2 direction = InputManager.WeaponBaseMoveInput;
        _body.forward = new Vector3(direction.x, 0, direction.y);
    }
}
