using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SWListener : MonoBehaviour
{
    [SerializeField] private SWTrigger _trigger;

    public HealthManager TargetHealthManager { get; private set; }

    private void OnEnable()
    {
        _trigger.OnTryFocusAction += OnTryFocus;
    }

    private void OnDisable()
    {
        _trigger.OnTryFocusAction -= OnTryFocus;
    }

    private void OnTryFocus(HealthManager healthManager)
    {
        if (TargetHealthManager == null)
        {
            TargetHealthManager = healthManager;
        }
    }
}
