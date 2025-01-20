using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HealthManager))]
public class TankStateMachine : MonoBehaviour
{
    [SerializeField] private Animator _headAnimator;
    [SerializeField] private Animator _bodyAnimator;
    [SerializeField] private float _moveSpeed = 10f;

    private StateMachine _headStateMachine;
    private StateMachine _bodyStateMachine;

    public HealthManager TankHealthManager { get; private set; }

    private bool _isBodyMove = false;
    private bool _isHeadFire = false;

    private void Awake()
    {
        TankHealthManager = GetComponent<HealthManager>();

        InitializeHeadStateMachine();
        InitializeBodyStateMachine();
    }

    private void InitializeHeadStateMachine()
    {
        TankHeadAnimationController headAnimationController = new TankHeadAnimationController(_headAnimator);

        State emptyState = new State();
        State fireState = new TankFireState(headAnimationController);

        emptyState.AddTransition(new StateTransition(fireState, new FuncStateCondition(() => _isHeadFire)));
        fireState.AddTransition(new StateTransition(emptyState, new FuncStateCondition(() => !_isHeadFire)));

        _headStateMachine = new StateMachine(emptyState);
    }

    private void InitializeBodyStateMachine()
    {
        TankBodyAnimationController bodynimationController = new TankBodyAnimationController(_bodyAnimator);

        State emptyState = new State();
        State moveState = new TankMoveState(transform, bodynimationController, _moveSpeed);

        emptyState.AddTransition(new StateTransition(moveState, new FuncStateCondition(() => _isBodyMove)));
        moveState.AddTransition(new StateTransition(emptyState, new FuncStateCondition(() => !_isBodyMove)));

        _bodyStateMachine = new StateMachine(emptyState);
    }

    private void Update()
    {
        _headStateMachine?.OnUpdate();
        _bodyStateMachine?.OnUpdate();
    }

    private void OnFixedUpdate()
    {
        _headStateMachine?.OnUpdate();
        _bodyStateMachine?.OnUpdate();
    }
}
