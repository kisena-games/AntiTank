using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankMoveState : State
{
    private readonly TankAudioManager _audioManager;
    private readonly NavMeshAgent _agent;
    private readonly TankAnimationController _controller;
    private readonly List<Vector3> _path;
    private readonly Vector3 _lastDestination;
    private readonly Transform _aimToAttack;
    private readonly string _name;
    private int _indexOfDestination = 0;

    public TankMoveState(string name, TankAudioManager audioManager, TankAnimationController controller, NavMeshAgent agent, List<Vector3> path, Vector3 lastDestination, Transform aimToAttack)
    {
        _name = name;
        _audioManager = audioManager;
        _agent = agent;
        _controller = controller;
        _path = path;
        _lastDestination = lastDestination;
        _aimToAttack = aimToAttack;
    }

    public override void OnEnter()
    {
        _controller.SetBool(TankAnimationType.MoveBool, true);
        _audioManager.PlayEngineForsage();
        _agent.SetDestination(_path[_indexOfDestination++]);
    }

    public override void OnExit()
    {
        _agent.velocity = Vector3.zero;
        _agent.transform.LookAt(_aimToAttack);
        _controller.SetBool(TankAnimationType.MoveBool, false);
    }

    public override void OnUpdate()
    {
        float distanceToTarget = Vector3.Distance(_agent.transform.position, _agent.destination);

        if (distanceToTarget <= _agent.stoppingDistance && _indexOfDestination < _path.Count)
        {
            //bool temp = _agent.pathPending;
            //bool temp2 = _agent.hasPath;
            //float distanceToTarget = Vector3.Distance(_agent.transform.position, _path[_indexOfDestination].position);
            //_agent.destination = _path[_indexOfDestination++].position;
            _agent.SetDestination(_path[_indexOfDestination++]);
        }
        else if (distanceToTarget <= _agent.stoppingDistance && _indexOfDestination >= _path.Count)
        {
            _agent.SetDestination(_lastDestination);
        }
    }
}
