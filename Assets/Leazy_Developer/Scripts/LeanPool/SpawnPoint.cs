using Lean.Pool;
using System;
using UnityEngine;

[Serializable]
public class SpawnPoint
{
    public TankPath path;
    public GameObject tankPrefab;
    public int tanksCount;
    public float spawnTime;

    private int _currentSpawnIndex = 0;
    private float _nextTimeToSpawn = 0.0f;

    public void UpdateSpawn()
    {
        if (Time.timeSinceLevelLoad >= _nextTimeToSpawn && _currentSpawnIndex < tanksCount)
        {
            var tank = LeanPool.Spawn(tankPrefab, path.SpawnPosition).GetComponent<TankStateMachine>();
            tank.Initialize(path);
            _currentSpawnIndex++;
            _nextTimeToSpawn = Time.timeSinceLevelLoad + spawnTime;
        }
    }
}