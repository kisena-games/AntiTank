using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable, IPauseHandler
{
    public bool IsKilled { get; private set; }

    protected Animator _animator;
    protected Collider _collider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
    }

    public void IsPaused(bool isPaused)
    {
        _animator.enabled = !isPaused;
    }

    public void TakeDamage(int damage)
    {
        _collider.enabled = false;
        IsKilled = true;

        OnTakeDamage(damage);
    }

    protected virtual void OnTakeDamage(int damage) { }
}
