using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

public class House : DamageableObject
{
    [SerializeField] private GameObject _defaultHouse;
    [SerializeField] private GameObject _alembicHouse;

    protected override void OnTakeDamage(int damage)
    {
        _defaultHouse.SetActive(false);
        _alembicHouse.SetActive(true);
    }
}
