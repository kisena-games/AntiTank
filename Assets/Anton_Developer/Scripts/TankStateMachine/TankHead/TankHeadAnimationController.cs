using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHeadAnimationController
{
    private readonly Animator _animator;

    private Dictionary<TankHeadAnimationType, int> animationTypeHash = new Dictionary<TankHeadAnimationType, int>();

    public Animator Animator => _animator;

    public TankHeadAnimationController(Animator animator)
    {
        _animator = animator;
        foreach (TankHeadAnimationType animationType in Enum.GetValues(typeof(TankHeadAnimationType)))
        {
            animationTypeHash.Add(animationType, Animator.StringToHash(animationType.ToString()));
        }
    }

    public void SetBool(TankHeadAnimationType animationType, bool value)
    {
        _animator.SetBool(animationTypeHash[animationType], value);
    }

    public void SetTrigger(TankHeadAnimationType animationType)
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

public enum TankHeadAnimationType
{
    FireBool
}
