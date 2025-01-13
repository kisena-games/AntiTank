using UnityEngine;

public class WeaponAttack : MonoBehaviour, ICanAttack
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPosition;

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
        Instantiate(_bulletPrefab, _bulletSpawnPosition.position, _bulletSpawnPosition.rotation);
    }
}
