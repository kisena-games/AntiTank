using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawnManager : MonoBehaviour
{
    public List<TanksWave> tanksWaves;

    private int _currentWaveIndex = 0;

    private void Update()
    {
        if (_currentWaveIndex < tanksWaves.Count)
        {
            if (Time.time >= tanksWaves[_currentWaveIndex].timeToWave)
            {
                _currentWaveIndex++;
            }
        }

        if (_currentWaveIndex > 0)
        {
            tanksWaves[_currentWaveIndex - 1].UpdateWave();
        }
    }
}
