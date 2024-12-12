using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FactoryClickUIController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private VisualElement rootElement;
    private VisualElement m_factoryMenu;
    private Label m_factoryNameLabel;
    private Label m_productivityLabel;
    private Button m_upgradeButton;
    private Button m_closeButton;

    private void Awake()
    {
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument n'est pas assigné dans l'inspecteur !");
            return;
        }
        rootElement = uiDocument.rootVisualElement;
        m_factoryMenu = rootElement.Q<VisualElement>("factory-menu");
        m_factoryNameLabel = m_factoryMenu.Q<Label>("factoryNameLabel");
        m_productivityLabel = m_factoryMenu.Q<Label>("productivityLabel");
        m_upgradeButton = m_factoryMenu.Q<Button>("upgradeButton");
        m_closeButton = m_factoryMenu.Q<Button>("closeButton");
        if (m_closeButton != null) m_closeButton.clicked += CloseMenu;
        if (m_factoryMenu != null) m_factoryMenu.style.display = DisplayStyle.None;
    }

    public void OpenMenu(Vector3 factoryPosition, string factoryName, float productivity)
    {
        if (m_factoryMenu == null)
        {
            Debug.LogError("Le menu d'usine n'est pas configuré correctement !");
            return;
        }
        m_factoryMenu.style.display = DisplayStyle.Flex;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(factoryPosition);
        m_factoryMenu.style.left = screenPos.x;
        m_factoryMenu.style.top = Screen.height - screenPos.y;
        if (m_factoryNameLabel != null) m_factoryNameLabel.text = "Nom de l'usine: " + factoryName;
        if (m_productivityLabel != null) m_productivityLabel.text = "Productivité: " + productivity.ToString("F1");
    }

    public void CloseMenu()
    {
        if (m_factoryMenu != null) m_factoryMenu.style.display = DisplayStyle.None;
    }
}
