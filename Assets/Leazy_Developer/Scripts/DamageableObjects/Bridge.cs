using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour, IDamageable, IPauseHandler
{
    [SerializeField] private int _maxHealth = 40;
    [SerializeField] private Animator _animator;

    private const string FALL_TRIGGER_KEY = "FallTrigger";

    private int _currentHealth;

    public bool IsKilled { get; private set; }

    protected Collider _collider;

    private void OnEnable()
    {
        GamePause.Instance?.AddPauseList(this);
    }

    private void OnDisable()
    {
        GamePause.Instance?.RemovePauseList(this);
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _currentHealth = _maxHealth;
    }

    public void IsPaused(bool isPaused)
    {
        _animator.enabled = !isPaused;
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
        _animator.SetTrigger(FALL_TRIGGER_KEY);
        _collider.enabled = false;
        IsKilled = true;
    }
}
