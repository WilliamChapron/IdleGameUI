using UnityEngine;
using UnityEngine.UIElements;

//------------------------------------------------------------------------------
public class SettingsPage : MonoBehaviour
{
    //--------------------------------------------------------------------------
    private UIDocument _UI_Document;
    private VisualElement _generalSettings;
    private VisualElement _audioSettings;
    private VisualElement _graphicsSettings;
    private VisualElement _languageSettings;

    //--------------------------------------------------------------------------
    private void Awake()
    {
        _UI_Document = GetComponent<UIDocument>();
        var root = _UI_Document.rootVisualElement;

        _generalSettings = root.Q("general-settings");
        _audioSettings = root.Q("audio-settings");
        _graphicsSettings = root.Q("graphics-settings");
        _languageSettings = root.Q("language-settings");
    }

    //--------------------------------------------------------------------------
    private void OnEnable()
    {
        var root = _UI_Document.rootVisualElement;

        root.Q<Button>("general-tab").clicked +=
            () => ShowPanel(_generalSettings);
        root.Q<Button>("audio-tab").clicked +=
            () => ShowPanel(_audioSettings);
        root.Q<Button>("graphics-tab").clicked +=
            () => ShowPanel(_graphicsSettings);
        root.Q<Button>("language-tab").clicked +=
            () => ShowPanel(_languageSettings);

        ShowPanel(_generalSettings);
    }

    //--------------------------------------------------------------------------
    private void OnDisable()
    {
        var root = _UI_Document.rootVisualElement;

        root.Q<Button>("general-tab").clicked -=
            () => ShowPanel(_generalSettings);
        root.Q<Button>("audio-tab").clicked -=
            () => ShowPanel(_audioSettings);
        root.Q<Button>("graphics-tab").clicked -=
            () => ShowPanel(_graphicsSettings);
        root.Q<Button>("language-tab").clicked -=
            () => ShowPanel(_languageSettings);
    }

    //--------------------------------------------------------------------------
    private void ShowPanel(VisualElement panelToShow)
    {
        Debug.Log($"Showing {panelToShow.name}");
        _generalSettings.style.display = DisplayStyle.None;
        _audioSettings.style.display = DisplayStyle.None;
        _graphicsSettings.style.display = DisplayStyle.None;
        _languageSettings.style.display = DisplayStyle.None;

        panelToShow.style.display = DisplayStyle.Flex;
    }
}