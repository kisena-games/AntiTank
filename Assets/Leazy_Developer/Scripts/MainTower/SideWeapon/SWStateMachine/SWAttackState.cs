using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWAttackState : State
{
    private Transform _transform;
    private Transform _headTransform;
    private float _attackMoveSpeed;
    private HealthManager _triggerHealth;

    public SWAttackState(List<HealthManager> _enemies, Transform transform, Transform headTransform, float speed)
    {
        _transform = transform;
        _headTransform = headTransform;
        _attackMoveSpeed = speed;
    }

    public override void OnUpdate()
    {
        _transform.LookAt(_triggerHealth.transform);
    }
}
