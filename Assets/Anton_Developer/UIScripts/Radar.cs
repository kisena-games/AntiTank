using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    [SerializeField] private RectTransform _radarUI;
    [SerializeField] private Image _tankIconPrefab;
    [SerializeField] private float _detectionRadius = 100f;

    private Transform _mainWeapon;
    private Dictionary<Transform, Image> _tanks = new Dictionary<Transform, Image>();


    private void Awake()
    {
        _mainWeapon = FindObjectOfType<MWBodyMovement>().transform;
    }

    private void Update()
    {
        foreach (var kvp in _tanks)
        {
            Transform tank = kvp.Key;
            Image tankIcon = kvp.Value;

            if (tank == null) continue;

            Vector3 directionToTank = tank.position - _mainWeapon.position;
            directionToTank = Quaternion.Inverse(_mainWeapon.rotation) * directionToTank;

            float distanceToTank = directionToTank.magnitude;
            float normalizedDistance = Mathf.Clamp01(distanceToTank / _detectionRadius);

            Vector2 radarPos = new Vector2(directionToTank.x, directionToTank.z).normalized * (normalizedDistance * 0.5f * _radarUI.rect.width);

            tankIcon.rectTransform.anchoredPosition = radarPos;
        }
    }

    public void RegisterTank(Transform tank)
    {
        if (!_tanks.ContainsKey(tank))
        {
            Image tankIcon = Instantiate(_tankIconPrefab, _radarUI);
            _tanks[tank] = tankIcon;
        }
    }

    public void UnRegisterTank(Transform tank)
    {
        if (_tanks.ContainsKey(tank))
        {
            Destroy(_tanks[tank].gameObject);
            _tanks.Remove(tank);
        }
    }
}
