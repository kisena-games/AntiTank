using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAnimationController
{
    private readonly Animator _animator;

    private readonly Dictionary<TankAnimationType, int> tankAnimationTypeHash = new Dictionary<TankAnimationType, int>();

    public Animator Animator => _animator;

    public TankAnimationController(Animator animator)
    {
        _animator = animator;
        foreach (TankAnimationType tankAnimationType in Enum.GetValues(typeof(TankAnimationType)))
        {
            tankAnimationTypeHash.Add(tankAnimationType, Animator.StringToHash(tankAnimationType.ToString()));
        }
    }

    public void SetBool(TankAnimationType tankAnimationType, bool value)
    {
        _animator.SetBool(tankAnimationTypeHash[tankAnimationType], value);
    }

    public void SetTrigger(TankAnimationType tankAnimationType)
    {
        _animator.SetTrigger(tankAnimationTypeHash[tankAnimationType]);
    }

    public void PlayAnimator()
    {
        _animator.enabled = true;
    }

    public void StopAnimator()
    {
        _animator.enabled = false;
    }
}

public enum TankAnimationType
{
    MoveBool,
    FireBool
}
