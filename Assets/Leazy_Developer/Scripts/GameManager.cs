using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Texture2D _cursorTexture;

    private void Awake()
    {
        Cursor.SetCursor(_cursorTexture, new Vector2(_cursorTexture.width / 2, _cursorTexture.height / 2), CursorMode.Auto);
    }
}
