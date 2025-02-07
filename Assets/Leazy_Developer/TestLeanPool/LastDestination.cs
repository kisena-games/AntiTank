using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastDestination : MonoBehaviour
{
    private Transform _tank;
    private TankHealth _tankHealth;

    public bool IsFree
    {
        get
        {
            if (_tank != null && _tankHealth.IsKilled)
            {
                _tank = null;
                _tankHealth = null;
            }

            if (_tank == null)
            {
                return true;
            }

            return false;
        }
    }

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
            _tankHealth = tank.GetComponent<TankHealth>();
            return true;
        }

        return false;
    }
}
