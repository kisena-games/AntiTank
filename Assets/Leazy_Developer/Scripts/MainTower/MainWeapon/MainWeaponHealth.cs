using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

public class MainWeaponHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 100;

    public int CurrentHealth { get; private set; }

    public bool IsKilled { get; private set; }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            IsKilled = true;
            Die();
        }
    }

    private void Die()
    {
        
    }
}
