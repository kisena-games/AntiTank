using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawnManager : MonoBehaviour
{
    [SerializeField] private LeanGameObjectPool pool;

    [Tooltip("Список всех волн на уровне")]
    public List<Wave> waves;

    private float _time;
    private float _nextSpawnWaveTime = 0f;
    private float _nextSpawnTankTime = 0f;

    private void Start()
    {
        
    }

    private void Update()
    {
        Debug.Log(_time);

        //if (Time.time >= _nextFireTime)
        //{
        //    _nextFireTime = Time.time + 1f / _fireRate;
        //}
    }
}

[Serializable]
public class Wave
{
    [Tooltip("Время в секундах между предыдущей волной и текущей")]
    public float timeToWave;

    [Tooltip("Список всех спавнов в волне")]
    public List<SpawnPosition> spawns;
}

[Serializable]
public class SpawnPosition
{
    [Tooltip("Первая точка спавна")]
    public Transform spawnPoint;

    [Tooltip("Далее точки назначения в пути")]
    public List<Transform> destinations;

    [Tooltip("Последняя точка назначения")]
    public List<Transform> lastDestinations;

    [Tooltip("Кучка танков, которые будут спавниться в точке спавна")]
    public TankHeap tankHeap;
}

[Serializable]
public class TankHeap
{
    [Tooltip("Вид танка")]
    public GameObject tank;

    [Tooltip("Кол-во танков")]
    public int count;

    [Tooltip("Скорость спавна в секундах (1 раз в ? секунд)")]
    public int timeToSpawn;
}
