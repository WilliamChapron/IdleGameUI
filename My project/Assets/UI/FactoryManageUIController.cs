using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FactoryManageUIController : MonoBehaviour
{
    [SerializeField] private UIDocument uIDocument; // Référence au UIDocument

    private VisualElement _generalDataContainer;
    private Button _toggleButton;

    private VisualElement _contentContainer;

    private bool _isOpen = true;

    private VisualElement m_category1Container;
    private VisualElement m_category2Container;
    private Button m_category1Button;
    private Button m_category2Button;

    void OnEnable()
    {

        var root = uIDocument.rootVisualElement;

        _generalDataContainer = root.Q<VisualElement>("factory-manage-container");
        _toggleButton = root.Q<Button>("toggle-button");

        _toggleButton.clicked += ToggleMenu;

        ToggleMenu();

        // Category system

        m_category1Container = root.Q("category1-container");
        m_category2Container = root.Q("category2-container");

        m_category1Button = root.Q<Button>("category1-button");
        m_category2Button = root.Q<Button>("category2-button");

        m_category1Container.style.display = DisplayStyle.Flex;
        m_category2Container.style.display = DisplayStyle.None;

        m_category1Button.clicked += () => ToggleCategory(1);
        m_category2Button.clicked += () => ToggleCategory(2);

        ToggleCategory(1);

    }

    void ToggleCategory(int categoryNumber)
    {
        if (categoryNumber == 1)
        {
            m_category1Container.style.display = DisplayStyle.Flex;
            m_category2Container.style.display = DisplayStyle.None;
        }
        else if (categoryNumber == 2)
        {
            m_category1Container.style.display = DisplayStyle.None;
            m_category2Container.style.display = DisplayStyle.Flex;
        }
    }

    private void ToggleMenu()
    {
        _isOpen = !_isOpen;

        if (_isOpen)
        {
            _generalDataContainer.RemoveFromClassList("closed");
            _toggleButton.RemoveFromClassList("closed");
        }
        else
        {
            _generalDataContainer.AddToClassList("closed");
            _toggleButton.AddToClassList("closed");
        }
    }


}
