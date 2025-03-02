using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

public class House : MonoBehaviour, IDamageable, IPauseHandler
{
    [SerializeField] private int _maxHealth = 40;
    [SerializeField] private GameObject _defaultHouse;
    [SerializeField] private GameObject _alembicHouse;
    [SerializeField] private Animator _animator;

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
        _collider.enabled = false;
        IsKilled = true;

        _defaultHouse.SetActive(false);
        _alembicHouse.SetActive(true);
    }
}