using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankDeadState : State
{
    private readonly TankAudioManager _audioManager;
    private readonly TankAnimationController _controller;
    private readonly ParticleSystem _particleSystem;

    public TankDeadState(TankAudioManager audioManager, TankAnimationController controller, ParticleSystem particleSystem)
    {
        _audioManager = audioManager;
        _controller = controller;
        _particleSystem = particleSystem;
    }

    public override void OnEnter()
    {
        _audioManager.PlayExplosion();
        _controller.SetBool(TankAnimationType.DeadBool, true);
        _particleSystem.Play();
    }

    public override void OnUpdate()
    {
        if (_particleSystem.isStopped)
        {
            LeanPool.Despawn(_audioManager.gameObject);
        }
    }
}
