using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTree : MonoBehaviour, IDamageable
{
    public bool IsKilled { get; private set; }

    private Animator _animator;
    private Collider _collider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
    }

    public void TakeDamage(int damage)
    {
        _collider.enabled = false;
        IsKilled = true;

        int randomFallValue = Random.Range(1, 4);
        _animator.SetFloat(TreeAnimationType.RandomFall.ToString(), randomFallValue);
    }
}

public enum TreeAnimationType
{
    RandomFall
}
