using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(TankHealth), typeof(NavMeshAgent))]
public class TankStateMachine : MonoBehaviour, IPauseHandler, IPoolable
{
    [SerializeField] private TankSO _tankSO;
    [SerializeField] private Animator _animator;

    //[Header("Move Parameters")]
    private TankPath _path;
    private Vector3 _lastDestination;

    private TankAudioManager _audioManager;
    private NavMeshAgent _agent;
    private StateMachine _stateMachine;
    private TankAnimationController _animationController;

    private Transform _aimToAttack;
    private float _distanceToLastDestination = 1f;
    private bool _isMove = false;

    private Vector3 _targetPosition = Vector3.zero;

    public void OnSpawn()
    {
        
    }

    public void OnDespawn()
    {
        _isMove = false;
    }

    public void Initialize(TankPath path)
    {
        _path = path;

        foreach (var lastDestin in _path.LastDestinations)
        {
            if (lastDestin.TryPlaceTank(gameObject.transform))
            {
                _lastDestination = lastDestin.transform.position;
                break;
            }
        }

        if (_lastDestination == null)
        {
            _lastDestination = _path.LastDestinations[0].transform.position;
        }

        InitializeStateMachine();

        _isMove = true;
    }

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
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (GamePause.Instance.IsPause)
        {
            return;
        }

        _distanceToLastDestination = Vector3.Distance(transform.position, _lastDestination);
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
        State moveState = new TankMoveState(transform.gameObject.ToString(), _audioManager, _animationController, _agent, _path.Destinations, _lastDestination);
        State fireState = new TankFireState(_audioManager, _animationController, _agent, transform, _aimToAttack, _tankSO.bulletSO);

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
