using System.Collections.Generic;
using System;

[Serializable]
public class TanksWave
{
    public float timeToWave;
    public List<SpawnPoint> spawnPoints;



    public void UpdateWave()
    {
        foreach (var point in spawnPoints)
        {
            point.UpdateSpawn();
        }
    }
}