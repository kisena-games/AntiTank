using UnityEngine;

public class CurrentStarsManager : MonoBehaviour
{
    [SerializeField] private GameObject _twoStars;
    [SerializeField] private GameObject _threeStars;

    private MainWeaponHealth _weaponHealth;

    private void Awake()
    {
        _weaponHealth = FindObjectOfType<MainWeaponHealth>();
    }
    private void OnEnable()
    {
        GameManager.OnWinAction += ShowCurrentHealth;
    }

    private void OnDisable()
    {
        GameManager.OnWinAction -= ShowCurrentHealth;
    }

    private void ShowCurrentHealth()
    {
        if (_weaponHealth.CurrentHealth >= 100)
        {
            _twoStars.SetActive(true);
        }

        if (_weaponHealth.CurrentHealth >= 200)
        {
            _threeStars.SetActive(true);
        }
    }
}
