using Lean.Pool;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab; 
    [SerializeField] private Transform _firePoint;       
    [SerializeField] private float _bulletSpeed = 10f;   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = LeanPool.Spawn(_bulletPrefab, _firePoint.position, _firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = _firePoint.forward * _bulletSpeed;
        }

        Destroy(bullet, 5f);
    }
}
