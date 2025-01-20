using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private bool _isCanMove = true;

    void Update()
    {
        if (_isCanMove)
        {
            transform.localPosition += transform.forward * _speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TriggerStop triggerStop))
        {
            _isCanMove = false;
        }
    }
}
