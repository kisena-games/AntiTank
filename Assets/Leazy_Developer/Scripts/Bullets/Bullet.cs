using Lean.Pool;
using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private float _flySpeed;
    [SerializeField] private int _damage;
    [SerializeField] private GameObject _bulletHole;

    private float _radius = 0.2f;
    private Rigidbody _rigidBody;
    private int _layerMask;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public void OnSpawn()
    {
        _rigidBody.velocity = transform.forward * _flySpeed;
    }

    public void OnDespawn()
    {
        _rigidBody.velocity = Vector3.zero;
    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _layerMask = LayerMask.NameToLayer("InvisibleWalls");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IDamageable damageableObject))
        {
            if (!damageableObject.IsKilled)
            {
                damageableObject.TakeDamage(_damage);
            }
        }

        if (collision.transform.gameObject.layer != _layerMask)
        {
            ContactPoint contactPoint = collision.contacts[0];
            Quaternion normalRotation = Quaternion.LookRotation(contactPoint.normal);

            Instantiate(_bulletHole, contactPoint.point, normalRotation, collision.transform);
        }

        ReturnToPool();
    }

    public void ReturnToPool()
    {
        LeanPool.Despawn(gameObject, 0.01f);
    }
}
