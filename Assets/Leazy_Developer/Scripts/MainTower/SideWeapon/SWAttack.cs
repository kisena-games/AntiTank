using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWAttack : MonoBehaviour
{
    private SWListener _SWListener;
    private List<HealthManager> _enemies;

    private void OnDisable()
    {
        
    }

    private void Awake()
    {
        _SWListener = GetComponent<SWListener>();
        _enemies = new List<HealthManager>();
    }

    private void Update()
    {
        
    }
}
