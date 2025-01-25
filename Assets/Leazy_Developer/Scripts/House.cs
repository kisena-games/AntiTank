using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

public class House : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject _defaultHouse;
    [SerializeField] private GameObject _alembicHouse;

    private Collider _collider;

    public bool IsKilled { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void TakeDamage(int damage)
    {
        _collider.enabled = false;
        IsKilled = true;

        _defaultHouse.SetActive(false);
        _alembicHouse.SetActive(true);
    }
}
