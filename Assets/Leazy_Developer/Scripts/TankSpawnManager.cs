using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawnManager : MonoBehaviour
{
    [SerializeField] private LeanGameObjectPool pool;

    [Tooltip("������ ���� ���� �� ������")]
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
    [Tooltip("����� � �������� ����� ���������� ������ � �������")]
    public float timeToWave;

    [Tooltip("������ ���� ������� � �����")]
    public List<SpawnPosition> spawns;
}

[Serializable]
public class SpawnPosition
{
    [Tooltip("������ ����� ������")]
    public Transform spawnPoint;

    [Tooltip("����� ����� ���������� � ����")]
    public List<Transform> destinations;

    [Tooltip("��������� ����� ����������")]
    public List<Transform> lastDestinations;

    [Tooltip("����� ������, ������� ����� ���������� � ����� ������")]
    public TankHeap tankHeap;
}

[Serializable]
public class TankHeap
{
    [Tooltip("��� �����")]
    public GameObject tank;

    [Tooltip("���-�� ������")]
    public int count;

    [Tooltip("�������� ������ � �������� (1 ��� � ? ������)")]
    public int timeToSpawn;
}
