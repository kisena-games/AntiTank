using Lean.Pool;
using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private float _flySpeed;
    [SerializeField] private int _damage;

    private float _radius = 0.2f;
    private Rigidbody _rigidBody;

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

        ReturnToPool();
    }

    public void ReturnToPool()
    {
        LeanPool.Despawn(gameObject);
    }
}
