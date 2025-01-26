using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWTrigger : MonoBehaviour
{
    public Action<Transform> OnTryFocusAction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthManager stateMachine))
        {
            OnTryFocusAction?.Invoke(stateMachine.transform);
        }
    }
}
