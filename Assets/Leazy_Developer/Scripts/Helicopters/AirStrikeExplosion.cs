using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStrikeExplosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticles;

    private void Start()
    {
        _explosionParticles.Play();
        Destroy(gameObject, 5f);
    }
}
