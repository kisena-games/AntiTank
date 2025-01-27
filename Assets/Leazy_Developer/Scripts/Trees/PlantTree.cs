using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTree : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject _tree;

    private Collider _collider;
    private Animator _animator;

    public bool IsKilled { get; private set; }

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
