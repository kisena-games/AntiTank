using Lean.Pool;
using UnityEngine;
using System.Collections;
using System;

public class WeaponAttack : MonoBehaviour, ICanAttack
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private ParticleSystem _defaultFireParticles;

    [Header("Default Mode Parameters")]
    [SerializeField] private Transform _bulletDefaultSpawnPosition;
    [SerializeField] private float _defaultFireRate = 0.0f;

    [Header("Sniper Mode Parameters")]
    [SerializeField] private Transform _bulletSniperSpawnPosition;
    [SerializeField] private float _sniperFireRate = 2f;
    [SerializeField] private int _sniperDamage = 60;

    private AudioSource _audioSource;
    private Coroutine _attackCoroutine;

    private float _nextDefaultFireTime = 0f;
    private float _nextSniperFireTime = 0f;

    private void OnEnable()
    {
        InputManager.AttackAction += Attack;
    }

    private void OnDisable()
    {
        InputManager.AttackAction -= Attack;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Attack()
    {
        if (GamePause.Instance.IsPause || _attackCoroutine != null)
        {
            return;
        }

        switch (SwitchCameraMode.CurrentMode)
        {
            case CameraMode.Default: DefaultAttack(); break;
            case CameraMode.Sniper: SniperAttack(); break;
        }
    }

    private void DefaultAttack()
    {
        if (Time.timeSinceLevelLoad >= _nextDefaultFireTime)
        {
            _defaultFireParticles.Play();
            _audioSource.PlayOneShot(_audioSource.clip);

            LeanPool.Spawn(_bulletPrefab, _bulletDefaultSpawnPosition.position, _bulletDefaultSpawnPosition.rotation);

            _nextDefaultFireTime = Time.timeSinceLevelLoad + 1f / _defaultFireRate;
        }
    }

    private void SniperAttack()
    {
        if (_sniperFireRate == 0f)
        {
            SniperShoot();
        }
        else if (Time.timeSinceLevelLoad >= _nextSniperFireTime)
        {
            SniperShoot();
            _nextSniperFireTime = Time.timeSinceLevelLoad + 1f / _sniperFireRate;
        }
    }

    private void SniperShoot()
    {
        _audioSource.PlayOneShot(_audioSource.clip);
        if (Physics.Raycast(new Ray(_bulletSniperSpawnPosition.position, _bulletSniperSpawnPosition.forward), out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out IDamageable damageableObject))
            {
                if (!damageableObject.IsKilled)
                {
                    damageableObject.TakeDamage(_sniperDamage);
                }
            }
        }
    }
}
