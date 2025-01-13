using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SWMovement : MonoBehaviour
{
    [SerializeField] private float _headMoveSpeed;
    [SerializeField] private float _bodyMoveSpeed;

    private SWListener _listener;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * 45f);
    }

    private void Awake()
    {
        _listener = GetComponent<SWListener>();
    }

    private void Update()
    {
        if (_listener.TargetHealthManager != null)
        {
            MoveAndAttackTarget();
        }
    }

    private void MoveAndAttackTarget()
    {
        
    }
}
