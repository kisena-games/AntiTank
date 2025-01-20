using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SWMovement : MonoBehaviour
{
    [SerializeField] private float _headMoveSpeed;
    [SerializeField] private float _bodyMoveSpeed;

    private SWListener _listener;

    private void Awake()
    {
        _listener = GetComponent<SWListener>();
    }

    private void Update()
    {
    }

    private void MoveAndAttackTarget()
    {
        
    }
}
