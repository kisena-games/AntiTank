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
    [SerializeField] private float _defaultAttackMultiplier = 1.0f;

    [Header("Sniper Mode Parameters")]
    [SerializeField] private Transform _bulletSniperSpawnPosition;
    [SerializeField] private float _sniperFireRate = 2f;
    [SerializeField] private float _sniperAttackMultiplier = 1.5f;

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
        if (_defaultFireRate == 0f)
        {
            _defaultFireParticles.Play();
            Shoot(_defaultAttackMultiplier, _bulletDefaultSpawnPosition);
        }
        else if (Time.timeSinceLevelLoad >= _nextDefaultFireTime)
        {
            _defaultFireParticles.Play();
            Shoot(_defaultAttackMultiplier, _bulletDefaultSpawnPosition);
            _nextDefaultFireTime = Time.timeSinceLevelLoad + 1f / _defaultFireRate;
        }
    }

    private void SniperAttack()
    {
        if (_sniperFireRate == 0f)
        {
            Shoot(_sniperAttackMultiplier, _bulletSniperSpawnPosition);
        }
        else if (Time.timeSinceLevelLoad >= _nextSniperFireTime)
        {
            Shoot(_sniperAttackMultiplier, _bulletSniperSpawnPosition);
            _nextSniperFireTime = Time.timeSinceLevelLoad + 1f / _sniperFireRate;
        }
    }

    private void Shoot(float attackMultiplier, Transform spawnPosition)
    {
        _audioSource.PlayOneShot(_audioSource.clip);
        Bullet bullet = LeanPool.Spawn(_bulletPrefab, spawnPosition.position, spawnPosition.rotation).GetComponent<Bullet>();
        bullet.Initialize(attackMultiplier);
    }
}
