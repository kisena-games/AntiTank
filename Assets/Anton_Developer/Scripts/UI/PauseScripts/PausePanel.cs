using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public void OnContinue()
    {
        GamePause.Instance.SetPause(false);
        gameObject.SetActive(false);
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnGoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
