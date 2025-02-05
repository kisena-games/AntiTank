using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastDestinationsManager : MonoBehaviour
{
    private List<LastDestination> _lastDestinations = new List<LastDestination>();

    private void Awake()
    {
        foreach (var destin in GetComponentsInChildren<LastDestination>())
        {
            _lastDestinations.Add(destin);
        }
    }

    public LastDestination GetFreeLastDestination()
    {
        foreach (var destin in _lastDestinations)
        {
            if (destin.IsFree)
            {
                return destin;
            }
        }

        return null;
    }
}
