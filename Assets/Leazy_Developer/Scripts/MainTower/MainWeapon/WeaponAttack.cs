using UnityEngine;

public class WeaponAttack : MonoBehaviour, ICanAttack
{
    [SerializeField] private GameObject _bulletPrefab;

    [Header("Default Mode Parameters")]
    [SerializeField] private Transform _bulletDefaultSpawnPosition;

    [Header("Sniper Mode Parameters")]
    [SerializeField] private Transform _bulletSniperSpawnPosition;

    private void OnEnable()
    {
        InputManager.AttackAction += Attack;
    }

    private void OnDisable()
    {
        InputManager.AttackAction -= Attack;
    }

    public void Attack()
    {
        if (GamePause.Instance.IsPause)
        {
            return;
        }

        switch (SwitchCameraMode.CurrentMode)
        {
            case CameraMode.Default: DefaultAttack(); break;
            case CameraMode.Sniper: SniperAttack(); break;
        }
    }

    public void DefaultAttack()
    {
        Instantiate(_bulletPrefab, _bulletDefaultSpawnPosition.position, _bulletDefaultSpawnPosition.rotation);
    }

    public void SniperAttack()
    {
        Instantiate(_bulletPrefab, _bulletSniperSpawnPosition.position, _bulletSniperSpawnPosition.rotation);
    }
}
