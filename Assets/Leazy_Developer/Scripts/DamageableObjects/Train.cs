using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 40;
    [SerializeField] private GameObject _defaultTrain;
    [SerializeField] private GameObject _boomedTrain;
    [SerializeField] private ParticleSystem _boomParticles;

    private int _currentHealth;

    public bool IsKilled { get; private set; }

    protected Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        _boomParticles.Play();

        _collider.enabled = false;
        IsKilled = true;

        _defaultTrain.SetActive(false);
        _boomedTrain.SetActive(true);
    }
}
