using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBodyAnimationController
{
    private readonly Animator _animator;

    private Dictionary<TankBodyAnimationType, int> animationTypeHash = new Dictionary<TankBodyAnimationType, int>();

    public Animator Animator => _animator;

    public TankBodyAnimationController(Animator animator)
    {
        _animator = animator;
        foreach (TankBodyAnimationType animationType in Enum.GetValues(typeof(TankBodyAnimationType)))
        {
            animationTypeHash.Add(animationType, Animator.StringToHash(animationType.ToString()));
        }
    }

    public void SetBool(TankBodyAnimationType animationType, bool value)
    {
        _animator.SetBool(animationTypeHash[animationType], value);
    }

    public void SetTrigger(TankBodyAnimationType animationType)
    {
        _animator.SetTrigger(animationTypeHash[animationType]);
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

public enum TankBodyAnimationType
{
    MoveBool
}
