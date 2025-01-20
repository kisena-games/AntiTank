using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWWaitState : State
{
    private Transform _transform;
    private Vector3 _originRotation;
    private float _waitMoveAngle;
    private float _waitMoveSpeed;

    private int side = 1;

    public SWWaitState(Transform transform, float angle, float speed)
    {
        _transform = transform;
        _originRotation = transform.rotation.eulerAngles;
        _waitMoveAngle = angle;
        _waitMoveSpeed = speed;
    }

    public override void OnUpdate()
    {
        float transformAngleY = _transform.rotation.eulerAngles.y;

        if (transformAngleY >= _originRotation.y + _waitMoveAngle || transformAngleY <= _originRotation.y -  _waitMoveAngle)
        {
            side *= -1;
        }

        float angle = side * _waitMoveAngle * (Time.deltaTime / _waitMoveSpeed);
        _transform.Rotate(Vector3.up, angle);
    }
}
