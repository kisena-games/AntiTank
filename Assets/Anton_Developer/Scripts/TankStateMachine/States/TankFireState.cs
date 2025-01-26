using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFireState : State
{
    private TankAnimationController _controller;

    public TankFireState(TankAnimationController controller)
    {
        _controller = controller;
    }

    public override void OnEnter()
    {
        _controller.SetBool(TankAnimationType.FireBool, true);
    }

    public override void OnExit()
    {
        _controller.SetBool(TankAnimationType.FireBool, false);
    }
}
