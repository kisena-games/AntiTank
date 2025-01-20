using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _flySpeed = 100f;
    [SerializeField] private int _damage = 10;

    private float _radius = 0.2f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private void Update()
    {
        transform.localPosition += transform.forward * _flySpeed * Time.deltaTime;           
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthManager healthManager))
        {
            healthManager.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }
}
