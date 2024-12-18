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
            Debug.LogError("mainUIDocument n'est pas assign� dans l'inspecteur !");
            return;
        }

        // V�rification de l'instance de GameManager
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance n'a pas �t� initialis� correctement !");
            return;
        }

        m_root = mainUIDocument.rootVisualElement;
        m_root.style.display = DisplayStyle.None;

        // S'abonner � l'�v�nement OnPauseStateChanged
        GameManager.Instance.OnPauseStateChanged += HandlePauseStateChanged;
    }

    private void OnDestroy()
    {
        // Se d�sabonner de l'�v�nement � la destruction de l'objet
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
