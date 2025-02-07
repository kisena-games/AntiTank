using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _tank_engine;
    [SerializeField] private AudioClip _tank_engine_forsage;
    [SerializeField] private AudioClip _tank_explosion;
    [SerializeField] private AudioClip _tank_shoot;

    public bool IsPlaying => _audioSource.isPlaying;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayEngineForsage()
    {
        _audioSource.PlayOneShot(_tank_engine_forsage);
        StartCoroutine(PlayEngineAfterForsage());
    }

    public void PlayShoot()
    {
        _audioSource.clip = _tank_shoot;
        _audioSource.PlayOneShot(_tank_shoot);
    }

    IEnumerator PlayEngineAfterForsage()
    {
        while (IsPlaying)
        {
            yield return null;
        }

        _audioSource.clip = _tank_engine;
        _audioSource.Play();
    }
}
