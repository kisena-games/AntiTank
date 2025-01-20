using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

[RequireComponent(typeof(SWListener))]
public class SWStateMachine : MonoBehaviour
{
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Transform _bodyTransform;

    [Header("Wait State Parameters")]
    [SerializeField] private float _waitMoveAngle = 45f;
    [SerializeField] private float _waitMoveSpeed = 5f;

    [Header("Attack State Parameters")]
    [SerializeField] private float _attackMoveSpeed = 5f;

    private StateMachine _stateMachine;
    private SWListener _SWListener;
    private List<HealthManager> _enemies;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(_headTransform.position, transform.forward * 45f);
    }

    private void Awake()
    {
        _SWListener = GetComponent<SWListener>();
        _enemies = new List<HealthManager>();
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        State waitState = new SWWaitState(transform, _waitMoveAngle, _waitMoveSpeed);
        State attackState = new SWAttackState(_enemies, transform, _headTransform, _attackMoveSpeed);

        _stateMachine = new StateMachine(waitState);
    }

    private void Update()
    {
        _stateMachine?.OnUpdate();
        _stateMachine?.OnUpdate();
    }

    private void FixedUpdate()
    {
        _stateMachine?.OnFixedUpdate();
        _stateMachine?.OnFixedUpdate();
    }
}
