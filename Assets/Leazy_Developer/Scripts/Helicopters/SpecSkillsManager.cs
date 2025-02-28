using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecSkillsManager : MonoBehaviour
{
    [Header("Air Strike Parameters")]
    [SerializeField] private GameObject _airStrikePrefab;
    [SerializeField] private Transform _airStrikeSpawnPosition;
    [SerializeField] private float _timeToChargeAirStrike = 20f;
    [SerializeField] private GameObject _airStrikeUI;

    private bool _isAirStrikeAvailable;

    private void Start()
    {
        StartCoroutine(AirStrikeCharge());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && _isAirStrikeAvailable)
        {
            _isAirStrikeAvailable = false;
            _airStrikeUI.SetActive(false);

            AirStrikeController airStrike = Instantiate(_airStrikePrefab, _airStrikeSpawnPosition.position, _airStrikeSpawnPosition.rotation).GetComponent<AirStrikeController>();
            StartCoroutine(DestroyAfterDelay(airStrike));
        }
    }

    IEnumerator AirStrikeCharge()
    {
        float time = Time.time + _timeToChargeAirStrike;

        while (Time.time < time)
        {
            yield return null;
        }

        _isAirStrikeAvailable = true;
        _airStrikeUI.SetActive(true);
    }

    IEnumerator DestroyAfterDelay(AirStrikeController airStrike)
    {
        yield return new WaitForSeconds(20f);
        Destroy(airStrike.gameObject);
        StartCoroutine(AirStrikeCharge());
    }
}
