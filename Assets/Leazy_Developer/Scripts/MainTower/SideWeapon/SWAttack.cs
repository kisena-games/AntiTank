using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWAttack : MonoBehaviour
{
    private SWListener _SWListener;
    private List<TankHealth> _tanks;

    private void OnDisable()
    {
        
    }

    private void Awake()
    {
        _SWListener = GetComponent<SWListener>();
        _tanks = new List<TankHealth>();
    }

    private void Update()
    {
        
    }
}
