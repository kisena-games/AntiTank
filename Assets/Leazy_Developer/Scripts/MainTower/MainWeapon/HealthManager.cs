using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _currentHealth;

    public bool IsDied => _currentHealth <= 0;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
    }
}
