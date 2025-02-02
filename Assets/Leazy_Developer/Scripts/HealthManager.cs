using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 100;

    private int _currentHealth;

    public bool IsDied => _currentHealth <= 0;

    public bool IsKilled { get; private set; }

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            IsKilled = true;
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
