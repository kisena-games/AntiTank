using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastDestination : MonoBehaviour
{
    private Transform _tank;

    public bool IsFree => _tank == null;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 2f);
    }

    public bool TryPlaceTank(Transform tank)
    {
        if (IsFree)
        {
            _tank = tank;
            return true;
        }

        return false;
    }
}
