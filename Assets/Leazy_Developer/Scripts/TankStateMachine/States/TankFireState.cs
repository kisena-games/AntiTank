using Lean.Pool;
using System;
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
    private readonly Transform _attackPoint;
    private readonly GameObject _bulletPrefab;
    private readonly float _fireRate;

    private float _nextFireTime = 0f;
    private Quaternion _targetRotation = Quaternion.identity;

    public TankFireState(TankAudioManager audioManager, TankAnimationController controller, NavMeshAgent agent, Transform attackPoint, Transform aimToAttack, GameObject bulletPrefab, float fireRate)
    {
        _audioManager = audioManager;
        _controller = controller;
        _agent = agent;
        _aimToAttack = aimToAttack;
        _attackPoint = attackPoint;
        _bulletPrefab = bulletPrefab;
        _fireRate = fireRate;
    }

    public override void OnEnter()
    {
        //_agent.updateRotation = false;
        Vector3 direction = (_aimToAttack.position - _attackPoint.position).normalized;
        _targetRotation = Quaternion.LookRotation(direction);

        _controller.SetBool(TankAnimationType.FireBool, true);
        
    }

    public override void OnExit()
    {
        _controller.SetBool(TankAnimationType.FireBool, false);
    }

    public override void OnUpdate()
    {
        if (Time.time >= _nextFireTime)
        {
            _audioManager.PlayShoot();
            Shoot();
            _nextFireTime = Time.time + 1f / _fireRate;
        }
    }

    private void Shoot()
    {
        LeanPool.Spawn(_bulletPrefab, _attackPoint.position, _targetRotation);
    }
}
