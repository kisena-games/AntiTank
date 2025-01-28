using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankFireState : State
{
    private readonly TankAudioManager _audioManager;
    private readonly TankAnimationController _controller;
    private readonly NavMeshAgent _agent;
    private readonly Transform _aimToAttack;
    private readonly Transform _tank;
    private readonly GameObject _bulettPrefab;

    Vector3 _targetDirection = Vector3.zero;

    public TankFireState(TankAudioManager audioManager, TankAnimationController controller, NavMeshAgent agent, Transform tank, Transform aimToAttack, GameObject bulettPrefab)
    {
        _audioManager = audioManager;
        _controller = controller;
        _agent = agent;
        _aimToAttack = aimToAttack;
        _tank = tank;
        _bulettPrefab = bulettPrefab;
        _targetDirection = (_aimToAttack.position - _tank.position);
    }

    public override void OnEnter()
    {
        _agent.updateRotation = false;
        _tank.rotation = Quaternion.FromToRotation(_tank.forward, -_targetDirection);
        _controller.SetBool(TankAnimationType.FireBool, true);
        _audioManager.PlayShoot();
    }

    public override void OnExit()
    {
        _controller.SetBool(TankAnimationType.FireBool, false);
    }

    public override void OnUpdate()
    {
        
    }
}
