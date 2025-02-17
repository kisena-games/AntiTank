using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPath : MonoBehaviour
{
    public Transform SpawnPosition { get; private set; }
    public List<Vector3> Destinations { get; private set; }
    public List<LastDestination> LastDestinations { get; private set; }

    private void Awake()
    {
        Destinations = new List<Vector3>();
        LastDestinations = new List<LastDestination>();

        var temp = GetComponentsInChildren<DrawDestination>();
        SpawnPosition = temp[0].transform;

        for (int i = 1; i < temp.Length; i++)
        {
            Destinations.Add(temp[i].transform.position);
        }

        foreach (var lastDestin in GetComponentsInChildren<LastDestination>())
        {
            LastDestinations.Add(lastDestin);
        }
    }

}
