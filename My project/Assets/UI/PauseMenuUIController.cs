using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuUIController : MonoBehaviour
{
    [SerializeField] private UIDocument mainUIDocument;

    private VisualElement m_root;

    void Start()
    {
        if (mainUIDocument == null)
        {
            Debug.LogError("mainUIDocument n'est pas assigné dans l'inspecteur !");
            return;
        }

        // Vérification de l'instance de GameManager
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance n'a pas été initialisé correctement !");
            return;
        }

        m_root = mainUIDocument.rootVisualElement;
        m_root.style.display = DisplayStyle.None;

        // S'abonner à l'événement OnPauseStateChanged
        GameManager.Instance.OnPauseStateChanged += HandlePauseStateChanged;
    }

    private void OnDestroy()
    {
        // Se désabonner de l'événement à la destruction de l'objet
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPauseStateChanged -= HandlePauseStateChanged;
        }
    }

    private void HandlePauseStateChanged(bool isPaused)
    {
        m_root.style.display = isPaused ? DisplayStyle.Flex : DisplayStyle.None;
    }
}
