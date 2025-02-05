using Lean.Pool;
using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private float _flySpeed;
    [SerializeField] private int _damage;

    private float _radius = 0.2f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public void OnSpawn() { }

    public void OnDespawn() { }

    private void Update()
    {
        transform.localPosition += transform.forward * _flySpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageableObject))
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
