using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankMoveState : State
{
    private readonly NavMeshAgent _agent;
    private readonly TankAnimationController _controller;
    private readonly List<Transform> _path;
    private int _indexOfDestination = 0;

    public TankMoveState(TankAnimationController controller, NavMeshAgent agent, List<Transform> path)
    {
        _agent = agent;
        _controller = controller;
        _path = path;
    }

    public override void OnEnter()
    {
        _controller.SetBool(TankAnimationType.MoveBool, true);
        _agent.SetDestination(_path[_indexOfDestination++].position);
    }

    public override void OnExit()
    {
        _controller.SetBool(TankAnimationType.MoveBool, false);
    }

    public override void OnUpdate()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance && _indexOfDestination < _path.Count)
        {
            _agent.SetDestination(_path[_indexOfDestination++].position);
        }
    }
}
