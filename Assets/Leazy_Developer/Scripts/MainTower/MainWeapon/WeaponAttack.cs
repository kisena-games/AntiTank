using Lean.Pool;
using UnityEngine;
using System.Collections;
using System;
using Cinemachine;

public class WeaponAttack : MonoBehaviour, ICanAttack
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _bulletHole;
    [SerializeField] private ParticleSystem _defaultFireParticles;
    [SerializeField] private LayerMask _buleltHoleExceptionLayers;

    [Header("Default Mode Parameters")]
    [SerializeField] private Transform _bulletDefaultSpawnPosition;

    [Header("Sniper Mode Parameters")]
    [SerializeField] private Transform _bulletSniperSpawnPosition;
    [SerializeField] private CinemachineVirtualCamera _cinemachineCamera;
    [SerializeField] private int _sniperDamage = 60;
    [SerializeField] private float _sniperFireRate = 0.7f;
    [SerializeField] private float _noiseDuration = 0.1f;

    private CinemachineBasicMultiChannelPerlin _cameraNoise;
    private AudioSource _audioSource;
    private Coroutine _attackCoroutine;

    private float _nextSniperFireTime = 0f;

    private int _layerMask;


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
        _layerMask = ~(1 << LayerMask.NameToLayer("InvisibleWalls"));
    }

    private void Start()
    {
        _cameraNoise = _cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
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
        _defaultFireParticles.Play();
        _audioSource.PlayOneShot(_audioSource.clip);

        LeanPool.Spawn(_bulletPrefab, _bulletDefaultSpawnPosition.position, _bulletDefaultSpawnPosition.rotation);
    }

    private void SniperAttack()
    {
        if (Time.timeSinceLevelLoad >= _nextSniperFireTime && _sniperFireRate != 0)
        {
            SniperShoot();
            _nextSniperFireTime = Time.timeSinceLevelLoad + 1f / _sniperFireRate;
        }
    }

    private void SniperShoot()
    {
        _audioSource.PlayOneShot(_audioSource.clip);

        if (Physics.Raycast(new Ray(_bulletSniperSpawnPosition.position, _bulletSniperSpawnPosition.forward), out RaycastHit hitInfo, Mathf.Infinity, _layerMask))
        {
            Debug.Log(hitInfo.collider.name);

            if (hitInfo.collider.TryGetComponent(out IDamageable damageableObject))
            {
                if (!damageableObject.IsKilled)
                {
                    damageableObject.TakeDamage(_sniperDamage);
                }
            }

            Quaternion normalRotation = Quaternion.LookRotation(hitInfo.normal);
            Instantiate(_bulletHole, hitInfo.point, normalRotation, hitInfo.transform);
        }

        StartCoroutine(Noize());
    }

    private IEnumerator Noize()
    {
        _cameraNoise.m_AmplitudeGain = 1.5f;
        _cameraNoise.m_FrequencyGain = 3.0f;

        yield return new WaitForSeconds(_noiseDuration);

        _cameraNoise.m_AmplitudeGain = 0f;
        _cameraNoise.m_FrequencyGain = 0f;
    }
}
