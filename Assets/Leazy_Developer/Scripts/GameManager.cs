using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Texture2D _defaultCursor;
    

    private void Awake()
    {
        Cursor.SetCursor(_defaultCursor, new Vector2(_defaultCursor.width / 2, _defaultCursor.height / 2), CursorMode.Auto);
    }


}
