using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class TabMenuController : MonoBehaviour
{
    [SerializeField] private string _menuContainerName;
    [SerializeField] private string _toggleMenuButtonName;
    [SerializeField] private string _navigationButtonsContainerName;
    [SerializeField] private string _tabsContainerName;

    private UIDocument UI_Document;

    private VisualElement _menuContainer;
    private Button _toggleMenuButton;

    private bool _isMenuOpen = true;

    private List<Button> _navigationButtons = new List<Button>();
    private List<Action> _navigationCallbacks;

    private List<VisualElement> _tabs = new List<VisualElement>();
    private VisualElement _activeTab = null;

    private void Awake()
    {
        UI_Document = GetComponent<UIDocument>();
        var root = UI_Document.rootVisualElement;

        _menuContainer = root.Q<VisualElement>(_menuContainerName);
        _toggleMenuButton = root.Q<Button>(_toggleMenuButtonName);

        /* Initialize tab data and logic */
        _navigationButtons = _menuContainer.Q<VisualElement>(_navigationButtonsContainerName).Children().OfType<Button>().ToList();
        _tabs = _menuContainer.Q<VisualElement>(_tabsContainerName).Children().ToList();

        _navigationCallbacks = new List<Action>();
        for(int i = 0; i < _navigationButtons.Count; i++)
        {
            var tabIndex = i;
            _navigationCallbacks.Add(() => ChangeActiveTab(tabIndex));
        }
    }

    void OnEnable()
    {
        for (int i = 0; i < _navigationButtons.Count; i++)
        {
            _navigationButtons[i].clicked += _navigationCallbacks[i];
        }
        _toggleMenuButton.clicked += ToggleMenu;

        _activeTab = _tabs[0];
        ToggleMenu();
        ChangeActiveTab(0);
    }

    private void OnDisable()
    {
        for (int i = 0; i < _navigationButtons.Count; i++)
        {
            _navigationButtons[i].clicked -= _navigationCallbacks[i];
        }

        _toggleMenuButton.clicked -= ToggleMenu;
    }

    void ChangeActiveTab(int tabTargetIndex)
    {
        _activeTab.style.display = DisplayStyle.None;
        _activeTab = _tabs[tabTargetIndex];
        _activeTab.style.display = DisplayStyle.Flex;
    }

    private void ToggleMenu()
    {
        _isMenuOpen = !_isMenuOpen;

        if (_isMenuOpen)
        {
            _menuContainer.RemoveFromClassList("closed");
            _toggleMenuButton.RemoveFromClassList("closed");
        }
        else
        {
            _menuContainer.AddToClassList("closed");
            _toggleMenuButton.AddToClassList("closed");
        }
    }
}
