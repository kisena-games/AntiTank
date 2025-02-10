using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawnManager : MonoBehaviour
{
    public static int tankCount = 0;

    public List<TanksWave> tanksWaves;

    private int _currentWaveIndex = 0;



    private void Awake()
    {
        
    }

    private void Update()
    {
        if (_currentWaveIndex < tanksWaves.Count)
        {
            if (Time.timeSinceLevelLoad >= tanksWaves[_currentWaveIndex].timeToWave)
            {
                _currentWaveIndex++;
            }
        }

        if (_currentWaveIndex > 0)
        {
            tanksWaves[_currentWaveIndex - 1].UpdateWave();
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log(tankCount);
    }
}
