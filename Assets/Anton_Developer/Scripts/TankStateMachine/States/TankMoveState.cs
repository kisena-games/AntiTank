using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMoveState : State
{
    private Transform _tankTransform;
    private TankAnimationController _controller;
    private float _moveSpeed;

    public TankMoveState(Transform tankTransform, TankAnimationController controller, float moveSpeed)
    {
        _tankTransform = tankTransform;
        _controller = controller;
    }

    public override void OnEnter()
    {
        _controller.SetBool(TankAnimationType.MoveBool, true);
    }

    public override void OnExit()
    {
        _controller.SetBool(TankAnimationType.MoveBool, false);
    }

    public override void OnUpdate()
    {
        _tankTransform.Translate(_tankTransform.forward * _moveSpeed * Time.deltaTime, Space.World);
    }
}
