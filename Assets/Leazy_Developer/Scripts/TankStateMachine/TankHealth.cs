using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour, IDamageable, IPoolable
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private Image _slider;

    private int _currentHealth;

    public bool IsKilled { get; private set; }

    public void OnSpawn()
    {
        _currentHealth = _maxHealth;
        IsKilled = false;
        _slider.fillAmount = _currentHealth / _maxHealth;
        _slider.enabled = true;
    }

    public void OnDespawn()
    {

    }

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _slider.fillAmount -= (float)damage / _maxHealth;

        if (_currentHealth <= 0)
        {
            IsKilled = true;
            Die();
        }
    }

    private void Die()
    {
        _slider.enabled = false;
    }
}
