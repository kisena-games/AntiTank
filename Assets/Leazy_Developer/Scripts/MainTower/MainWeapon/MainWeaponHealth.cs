using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainWeaponHealth : MonoBehaviour, IDamageable
{
    public static Action OnLoseAction;

    [SerializeField] private TextMeshProUGUI _healthText;

    [SerializeField] private ParticleSystem _smokeParticles;
    [SerializeField] private ParticleSystem _electricParticles;
    [SerializeField] private ParticleSystem _boomParticles;
    [SerializeField] private ParticleSystem _deathParticles;

    [field: SerializeField] public int MaxHealth { get; private set; }

    private float[] _partsOfHealth = new float[3]{ 20, 40, 75 };
    private int _indexPart;

    public int CurrentHealth { get; private set; }
    public bool IsKilled { get; private set; }
    

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
        CurrentHealth = MaxHealth;
        _healthText.text = CurrentHealth.ToString();
    }

    private void OnCheatHealth(int cheatHealth)
    {
        _smokeParticles.Stop();
        _electricParticles.Stop();
        _boomParticles.Stop();

        CurrentHealth = cheatHealth;
        _healthText.text = CurrentHealth.ToString();
        Debug.Log("1000 health is success. Relax and enjoy!");
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        _healthText.text = CurrentHealth.ToString();

        float tempPart = (float)CurrentHealth / MaxHealth;

        switch (_indexPart)
        {
            case 0:
                if (tempPart <= _partsOfHealth[2] / 100f)
                {
                    _smokeParticles.Play();
                    _indexPart++;
                }
                break;
            case 1:
                if (tempPart <= _partsOfHealth[1] / 100f)
                {
                    _electricParticles.Play();
                    _indexPart++;
                }
                break;
            case 2:
                if (tempPart <= _partsOfHealth[0] / 100f)
                {
                    _boomParticles.Play();
                    _indexPart++;
                }
                break;
            default: break;
        }

        if (CurrentHealth <= 0)
        {
            IsKilled = true;
            Die();
        }
    }

    private void Die()
    {
        _deathParticles.Play();
        OnLoseAction?.Invoke();
    }
}
