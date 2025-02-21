using Lean.Pool;
using System;
using System.Collections.Generic;
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

    private void OnEnable()
    {
        CheatsInputManager.CheatHealthAction += OnCheatHealth;
    }

    private void OnDisable()
    {
        CheatsInputManager.CheatHealthAction -= OnCheatHealth;
    }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    private void Start()
    {
        _healthText.text = CurrentHealth.ToString();
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        _healthText.text = CurrentHealth.ToString();

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

    private void OnCheatHealth(int cheatHealth)
    {
        CurrentHealth = cheatHealth;
        _healthText.text = CurrentHealth.ToString();
        Debug.Log("1000 health is success. Relax and enjoy!");
    }
}
