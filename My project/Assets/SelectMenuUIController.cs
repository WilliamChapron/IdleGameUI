using UnityEngine;
using UnityEngine.UIElements;

public class SelectMenuUIController : MonoBehaviour
{
    [SerializeField] UIDocument m_uiDocument;
    [SerializeField] private Texture2D handCursorTexture;


    //void Start()
    //{
    //    var rootVisualElement = m_uiDocument.rootVisualElement;

        
    //}

    //// Cursor func
    //private void AddCursorCallbacks(Button button)
    //{
    //    button.RegisterCallback<MouseEnterEvent>(evt => ActivateCursor());
    //    button.RegisterCallback<MouseLeaveEvent>(evt => DeactivateCursor());
    //}

    //private void ActivateCursor()
    //{
    //    UnityEngine.Cursor.SetCursor(handCursorTexture, Vector2.zero, CursorMode.Auto);
    //}

    //private void DeactivateCursor()
    //{
    //    UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    //}

    //// Close menu
    //private void CloseMenu()
    //{
    //    m_uiDocument.enabled = false;
    //}

}
