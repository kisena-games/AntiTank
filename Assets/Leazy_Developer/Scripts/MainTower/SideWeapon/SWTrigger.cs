using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWTrigger : MonoBehaviour
{
    public Action<TankHealth> OnTryFocusAction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TankHealth healthManager))
        {
            OnTryFocusAction?.Invoke(healthManager);
        }
    }
}
