using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HealthManager), typeof(NavMeshAgent))]
public class TankStateMachine : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private StateMachine _stateMachine;

    public HealthManager TankHealthManager { get; private set; }

    private bool _isMove = false;
    private bool _isFire = false;

    private void Awake()
    {
        TankHealthManager = GetComponent<HealthManager>();

        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        TankAnimationController headAnimationController = new TankAnimationController(_animator);

        State emptyState = new State();
        //State moveState = new TankMoveState(transform, );
        State fireState = new TankFireState(headAnimationController);

        emptyState.AddTransition(new StateTransition(fireState, new FuncStateCondition(() => _isFire)));
        fireState.AddTransition(new StateTransition(emptyState, new FuncStateCondition(() => !_isFire)));

        _stateMachine = new StateMachine(emptyState);
    }

    private void Update()
    {
        _stateMachine?.OnUpdate();
    }

    private void FixedUpdate()
    {
        _stateMachine?.OnFixedUpdate();
    }
}
