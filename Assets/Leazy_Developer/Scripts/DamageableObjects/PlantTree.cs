using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTree : DamageableObject
{
    [SerializeField] private GameObject _tree;

    protected override void OnTakeDamage(int damage)
    {
        int randomFallValue = Random.Range(1, 4);
        _animator.SetFloat(TreeAnimationType.RandomFall.ToString(), randomFallValue);
    }
}

public enum TreeAnimationType
{
    RandomFall
}
