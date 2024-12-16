using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;

enum Settings
{
    General,
    Audio,
    Graphics,
    Language
}

//------------------------------------------------------------------------------
public class SettingsPage : MonoBehaviour
{
    //--------------------------------------------------------------------------
    private UIDocument _UI_Document;

    private Settings _activeTabKey;

    private Dictionary<Settings, VisualElement> _tabs = new Dictionary<Settings, VisualElement>();
    private Dictionary<Settings, Button> _tabButtons = new Dictionary<Settings, Button>();



    //--------------------------------------------------------------------------
    private void Awake()
    {
        _UI_Document = GetComponent<UIDocument>();
        var root = _UI_Document.rootVisualElement;

        /* Fetch tabs and buttons */
        _tabs[Settings.General] = root.Q("general-tab");
        _tabs[Settings.Audio] = root.Q("audio-tab");
        _tabs[Settings.Graphics] = root.Q("graphics-tab");
        _tabs[Settings.Language] = root.Q("language-tab");

        _tabButtons[Settings.General] = root.Q("general-tab-button") as Button;
        _tabButtons[Settings.Audio] = root.Q("audio-tab-button") as Button;
        _tabButtons[Settings.Graphics] = root.Q("graphics-tab-button") as Button;
        _tabButtons[Settings.Language] = root.Q("language-tab-button") as Button;

        /* Make sure all tabs are hidden */
        foreach (var tab in _tabs)
        {
            tab.Value.style.display = DisplayStyle.None;
        }
    }

    //--------------------------------------------------------------------------
    private void OnEnable()
    {
        foreach (var button in _tabButtons)
        {
            button.Value.clicked += () => ShowTab(button.Key);
        }

        ShowTab(Settings.General);
    }

    //--------------------------------------------------------------------------
    private void OnDisable()
    {
        foreach (var button in _tabButtons)
        {
            button.Value.clicked -= () => ShowTab(button.Key);
        }
    }

    //--------------------------------------------------------------------------
    private void ShowTab(Settings settingToShow)
    {
        /* Hide current tab */
        _tabs[_activeTabKey].style.display = DisplayStyle.None;

        /* Show new tab */
        _activeTabKey = settingToShow;
        _tabs[_activeTabKey].style.display = DisplayStyle.Flex;
    }
}