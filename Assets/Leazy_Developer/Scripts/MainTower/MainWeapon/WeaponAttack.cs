using Lean.Pool;
using UnityEngine;
using System.Collections;

public class WeaponAttack : MonoBehaviour, ICanAttack
{
    [SerializeField] private GameObject _bulletPrefab;
    

    [Header("Default Mode Parameters")]
    [SerializeField] private Transform _bulletDefaultSpawnPosition;
    [SerializeField] private float _defaultAttackInterval=0.5f;
    [Header("Sniper Mode Parameters")]
    [SerializeField] private Transform _bulletSniperSpawnPosition;
    [SerializeField] private float _sniperAttackInterval = 0.5f;

    private AudioSource _audioSource;
    private Coroutine _attackCoroutine;

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
            case CameraMode.Default:
                _attackCoroutine = StartCoroutine(DefaultAttack());
                break;
            case CameraMode.Sniper:
                _attackCoroutine = StartCoroutine(SniperAttack());
                break;
        }
    }

    private IEnumerator DefaultAttack()
    {
        _audioSource.PlayOneShot(_audioSource.clip);
        GameObject bullet = LeanPool.Spawn(_bulletPrefab, _bulletDefaultSpawnPosition.position, _bulletDefaultSpawnPosition.rotation);

        yield return new WaitForSeconds(_defaultAttackInterval);

        _attackCoroutine = null; // ”станавливаем корутину в null, когда она закончена
    }

    private IEnumerator SniperAttack()
    {
        _audioSource.PlayOneShot(_audioSource.clip);
        GameObject bullet = LeanPool.Spawn(_bulletPrefab, _bulletSniperSpawnPosition.position, _bulletSniperSpawnPosition.rotation);

        yield return new WaitForSeconds(_sniperAttackInterval);

        _attackCoroutine = null; // ”станавливаем корутину в null, когда она закончена
    }
}
