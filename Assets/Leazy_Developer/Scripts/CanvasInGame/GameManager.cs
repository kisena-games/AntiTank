using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Texture2D _defaultCursor;
    [SerializeField] private int _maxNumberForStrings = 300;
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private TextMeshProUGUI _currentTanksCount;
    [SerializeField] private TextMeshProUGUI _maxTanksCount;

    public static int MaxTanksCount { get; private set; }
    public static int KilledTanksCount { get; private set; }
    public static string[] StringNumbers { get; private set; }

    public static Action OnWinAction;
    public static Action OnChangeTankCountAction;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        KilledTanksCount = 0;
        StringNumbers = new string[_maxNumberForStrings + 1];
        for (int i = 0; i < StringNumbers.Length; i++)
        {
            StringNumbers[i] = i.ToString();
        }

        Cursor.SetCursor(_defaultCursor, new Vector2(_defaultCursor.width / 2, _defaultCursor.height / 2), CursorMode.Auto);

        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        DynamicGI.UpdateEnvironment();
        StartCoroutine(WinListener());
        _maxTanksCount.text = string.Format("/{0}", GameManager.StringNumbers[MaxTanksCount]);
    }

    public static void SetMaxTanksCount(int count)
    {
        MaxTanksCount = count;
    }

    public static void OnTankKilled()
    {
        KilledTanksCount++;
        OnChangeTankCountAction?.Invoke();
    }

    public void OnContinue()
    {
        PlayButtonSound();
        GamePause.Instance.SetPause(false);
    }

    public void OnRestart()
    {
        PlayButtonSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnNextLevel()
    {
        PlayButtonSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnGoToMenu()
    {
        PlayButtonSound();
        SceneManager.LoadScene(0);
    }

    public void SetPause(bool isPause)
    {
        if (isPause)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(_defaultCursor, new Vector2(_defaultCursor.width / 2, _defaultCursor.height / 2), CursorMode.Auto);

            if (SwitchCameraMode.CurrentMode == CameraMode.Sniper)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }

    private void PlayButtonSound()
    {
        _audioSource.PlayOneShot(_buttonClick);
    }

    IEnumerator WinListener()
    {
        while (KilledTanksCount != MaxTanksCount)
        {
            yield return new WaitForSeconds(1f);
        }

        OnWinAction?.Invoke();
    }
}
