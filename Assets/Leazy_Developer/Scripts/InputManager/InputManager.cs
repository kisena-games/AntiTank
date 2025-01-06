using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 WeaponBaseMoveInput { get; private set; } = Vector2.zero;
    public static bool IsWeaponBaseMoving { get; private set; } = false;
    public static Vector2 WeaponTopMoveInput { get; private set; } = Vector2.zero;
    public static bool IsWeaponTopMoving { get; private set; } = false;

    public static Action AttackAction;

    public void OnWeaponMoveBase(InputAction.CallbackContext context)
    {
        if (context.performed && !IsWeaponBaseMoving)
        {
            IsWeaponBaseMoving = true;
        }
        else if (context.canceled && IsWeaponBaseMoving)
        {
            IsWeaponBaseMoving = false;
        }

        WeaponBaseMoveInput = context.ReadValue<Vector2>();
    }

    public void OnWeaponMoveTopDelta(InputAction.CallbackContext context)
    {
        if (context.performed && !IsWeaponTopMoving)
        {
            IsWeaponTopMoving = true;
        }
        else if (context.canceled && IsWeaponTopMoving)
        {
            IsWeaponTopMoving = false;
        }
    }

    public void OnWeaponMoveTopInput(InputAction.CallbackContext context)
    {
        WeaponTopMoveInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AttackAction?.Invoke();
        }
    }
}
