using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SWListener : MonoBehaviour
{
    [SerializeField] private Transform _bodyTransform;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private List<SWTrigger> _triggers;

    [Header("Rotation Parameters")]
    [SerializeField] private float _rotationSpeed = 50f;

    [Header("Shoot Parameters")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _fireRate = 2f; // ������� �������� � ��������� � �������

    private Quaternion _originBodyRotation;
    private Transform _targetTank;

    private List<Transform> _enemies;

    private SWState _state = SWState.Wait;

    private float _nextFireTime = 0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(_headTransform.position, _headTransform.forward * 45f);
    }

    private void OnEnable()
    {
        foreach (var _trigger in _triggers)
        {
            _trigger.OnTryFocusAction += OnTryFocus;
        }
    }

    private void OnDisable()
    {
        foreach (var _trigger in _triggers)
        {
            _trigger.OnTryFocusAction -= OnTryFocus;
        }
    }

    private void Awake()
    {
        _enemies = new List<Transform>();
        _originBodyRotation = _bodyTransform.rotation;
    }

    private void Update()
    {
        CheckDiedTanks();

        if (_targetTank == null && _enemies.Count > 0)
        {
            _targetTank = _enemies[0];
            _state = SWState.Attack;
        }

        if (_targetTank != null && _state == SWState.Attack)
        {
            RotateBodyTowardsEnemy();
            RotateHeadTowardsEnemy();

            if (Physics.Raycast(new Ray(_headTransform.position, _headTransform.forward), out RaycastHit hitInfo))
            {
                if (hitInfo.collider.TryGetComponent(out HealthManager healthManager))
                {
                    if (healthManager.transform == _targetTank && Time.time >= _nextFireTime)
                    {
                        Shoot();
                        _nextFireTime = Time.time + 1f / _fireRate;
                    }
                }
            }
        }
        else if (_targetTank == null && _state == SWState.Attack)
        {
            _state = SWState.Wait;
            StartCoroutine(ResetBodyRotation());
            _headTransform.localRotation = Quaternion.identity;
        }
    }

    private void CheckDiedTanks()
    {
        _enemies.RemoveAll(enemy => enemy == null);
    }

    private void Shoot()
    {
        Instantiate(_bulletPrefab, _headTransform.position, _headTransform.rotation);
    }

    private void RotateBodyTowardsEnemy()
    {
        Vector3 direction = _targetTank.position - _bodyTransform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _bodyTransform.rotation = Quaternion.RotateTowards(_bodyTransform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void RotateHeadTowardsEnemy()
    {
        Vector3 relativePosition = _targetTank.position - _headTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(relativePosition, Vector3.up);
        _headTransform.rotation = Quaternion.Euler(targetRotation.eulerAngles.x, _headTransform.rotation.eulerAngles.y, _headTransform.rotation.eulerAngles.z);
    }

    private void OnTryFocus(Transform tank)
    {
        if (tank != null)
        {
            _enemies.Add(tank);
        }
    }

    private IEnumerator ResetBodyRotation()
    {
        while (_bodyTransform.rotation != _originBodyRotation)
        {
            if (_state == SWState.Attack)
            {
                StopAllCoroutines();
            }

            _bodyTransform.rotation = Quaternion.RotateTowards(_bodyTransform.rotation, _originBodyRotation, _rotationSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}

public enum SWState
{
    Wait,
    Attack
}
