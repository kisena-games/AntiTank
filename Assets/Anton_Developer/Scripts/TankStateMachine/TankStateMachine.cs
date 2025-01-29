using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HealthManager), typeof(NavMeshAgent))]
public class TankStateMachine : MonoBehaviour, IPauseHandler
{
    [SerializeField] private Animator _animator;

    [Header("Move Parameters")]
    [SerializeField] private List<Transform> _path;

    [Header("Attack Parameters")]
    [SerializeField] private GameObject _bulettPrefab;

    private TankAudioManager _audioManager;
    private Transform _aimToAttack;
    private NavMeshAgent _agent;
    private StateMachine _stateMachine;
    private TankAnimationController _animationController;
    private float _distanceToLastDestination = 1f;
    private bool _isMove = false;

    private Vector3 _targetPosition = Vector3.zero;

    private void OnEnable()
    {
        GamePause.Instance?.AddPauseList(this);
    }

    private void OnDisable()
    {
        GamePause.Instance?.RemovePauseList(this);
    }

    private void Awake()
    {
        _aimToAttack = FindObjectOfType<MWHeadMovement>().transform;
        _agent = GetComponent<NavMeshAgent>();
        _audioManager = GetComponent<TankAudioManager>();
        InitializeStateMachine();
    }

    private void Start()
    {
        _isMove = true;
    }

    private void Update()
    {
        if (GamePause.Instance.IsPause)
        {
            return;
        }

        _distanceToLastDestination = Vector3.Distance(transform.position, _path[_path.Count - 1].position);
        _stateMachine?.OnUpdate();
    }

    private void FixedUpdate()
    {
        if (GamePause.Instance.IsPause)
        {
            return;
        }

        _stateMachine?.OnFixedUpdate();
    }

    private void InitializeStateMachine()
    {
        _animationController = new TankAnimationController(_animator);

        State emptyState = new State();
        State moveState = new TankMoveState(transform.gameObject.ToString(), _audioManager, _animationController, _agent, _path);
        State fireState = new TankFireState(_audioManager, _animationController, _agent, transform, _aimToAttack, _bulettPrefab);

        emptyState.AddTransition(new StateTransition(moveState, new FuncStateCondition(() => _isMove)));
        moveState.AddTransition(new StateTransition(fireState, new FuncStateCondition(() => _distanceToLastDestination <= _agent.stoppingDistance)));

        _stateMachine = new StateMachine(emptyState);
    }

    public void IsPaused(bool isPaused)
    {
        if (isPaused)
        {
            _animationController.StopAnimator();
            _agent.isStopped = true;
            _targetPosition = _agent.destination;
            _agent.Warp(_agent.transform.position);
        }
        else
        {
            _animationController.PlayAnimator();
            _agent.isStopped = false;
            _agent.SetDestination(_targetPosition);
        }
        
    }
}
