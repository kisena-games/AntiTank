using Lean.Pool;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainWeaponHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private TextMeshProUGUI _healthText;

    public int CurrentHealth { get; private set; }
    public bool IsKilled { get; private set; }

    public static Action OnLoseAction;

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    private void Start()
    {
        _healthText.text = GameManager.StringNumbers[CurrentHealth];
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        _healthText.text = GameManager.StringNumbers[CurrentHealth];

        if (CurrentHealth <= 0)
        {
            IsKilled = true;
            Die();
        }
    }

    private void Die()
    {
        OnLoseAction?.Invoke();
    }
}
