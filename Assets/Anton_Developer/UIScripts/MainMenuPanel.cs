using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] private Button _quit;

    private void OnEnable()
    {
        _quit.onClick.AddListener(OnExitButtonClick);

    }

    private void OnDisable()
    {
        _quit.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}
