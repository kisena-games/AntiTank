using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFireState : State
{
    private TankHeadAnimationController _controller;

    public TankFireState(TankHeadAnimationController controller)
    {
        _controller = controller;
    }

    public override void OnEnter()
    {
        _controller.SetBool(TankHeadAnimationType.FireBool, true);
    }

    public override void OnExit()
    {
        _controller.SetBool(TankHeadAnimationType.FireBool, false);
    }
}
