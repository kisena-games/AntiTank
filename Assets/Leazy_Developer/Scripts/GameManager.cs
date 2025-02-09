using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D _defaultCursor;
    [SerializeField] private int _maxNumberForStrings = 300;

    public static int MaxTanksCount { get; private set; }
    public static int KilledTanksCount { get; private set; }
    public static string[] StringNumbers { get; private set; }

    private void Awake()
    {
        StringNumbers = new string[_maxNumberForStrings + 1];
        for (int i = 0; i < StringNumbers.Length; i++)
        {
            StringNumbers[i] = i.ToString();
        }

        Cursor.SetCursor(_defaultCursor, new Vector2(_defaultCursor.width / 2, _defaultCursor.height / 2), CursorMode.Auto);
    }

    public static void SetTanksCount(int count)
    {
        MaxTanksCount = count;
    }

    public static void OnTankKilled()
    {
        KilledTanksCount++;
    }

    public void OnContinue()
    {
        GamePause.Instance.SetPause(false);
        gameObject.SetActive(false);
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnGoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
