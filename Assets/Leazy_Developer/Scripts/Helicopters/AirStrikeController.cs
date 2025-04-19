using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirStrikeController : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private float _timeToStartAttack = 3f;
    [SerializeField] private float _attackDuration = 5f;
    [SerializeField] private float _helicopterSpeed = 5f;

    private Vector3 _terrainPosition;
    private Vector2 _terrainSize;

    private void Awake()
    {
        Terrain terrain = FindObjectOfType<Terrain>();
        _terrainSize = new Vector2(terrain.terrainData.size.x, terrain.terrainData.size.z);
        _terrainPosition = terrain.transform.position;
    }

    private void Start()
    {
        StartCoroutine(StartAirStrike());
    }

    private void Update()
    {
        transform.position += transform.forward * _helicopterSpeed * Time.deltaTime;
    }

    IEnumerator StartAirStrike()
    {
        yield return new WaitForSeconds(_timeToStartAttack);

        float endTime = Time.time + _attackDuration;

        while (Time.time < endTime)
        {
            float randomX = Random.Range(_terrainPosition.x, _terrainPosition.x + _terrainSize.x);
            float randomY = Random.Range(_terrainPosition.z, _terrainPosition.z + _terrainSize.y);
            Vector3 randomPosition = new Vector3(randomX, 0, randomY);

            Instantiate(_explosionPrefab, randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(0.1f);
        }

        TankHealth[] allTanks = FindObjectsOfType<TankHealth>(false);

        foreach (var tank in allTanks)
        {
            tank.TakeDamage(1000);
        }
    }
}
