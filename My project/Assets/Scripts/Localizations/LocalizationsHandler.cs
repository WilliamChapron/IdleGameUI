using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;

//------------------------------------------------------------------------------
public class LocalizationsHandler : MonoBehaviour
{
    //--------------------------------------------------------------------------
    private UIDocument UI_Document;

    //--------------------------------------------------------------------------
    private void OnEnable()
    {
        UI_Document = GetComponent<UIDocument>();
        var root = UI_Document.rootVisualElement;

        Activate<LocalizedLabel>(root);
        Activate<LocalizedButton>(root);
        Activate<LocalizedToggle>(root);
    }

    //--------------------------------------------------------------------------
    private void OnDisable()
    {
        var root = UI_Document.rootVisualElement;
        Deactivate<LocalizedLabel>(root);
        Deactivate<LocalizedButton>(root);
        Deactivate<LocalizedToggle>(root);

        UI_Document = null;
    }

    //--------------------------------------------------------------------------
    private void Activate<T>(VisualElement root)
        where T : VisualElement, ILocalizedElement
    {
        LocalizeAll<T>(root);
        LocalizationSettings.SelectedLocaleChanged += _ => LocalizeAll<T>(root);
    }

    //--------------------------------------------------------------------------
    private void Deactivate<T>(VisualElement root)
        where T : VisualElement, ILocalizedElement
    {
        LocalizationSettings.SelectedLocaleChanged -= _ => LocalizeAll<T>(root);
    }

    //--------------------------------------------------------------------------
    private void LocalizeAll<T>(VisualElement root)
        where T : VisualElement, ILocalizedElement
    {
        var localizedElements = root.Query<T>().ToList();

        foreach (T localizedElement in localizedElements)
        {
            /*if (string.IsNullOrEmpty(localizedElement.localizationKey) ||
                string.IsNullOrEmpty(localizedElement.localizationTable)) 
                continue;*/

            var localizedString = new LocalizedString
            {
                TableReference = localizedElement.localizationTable,
                TableEntryReference = localizedElement.localizationKey
            };

            localizedString.StringChanged += (localizedText) =>
            {
                localizedElement.text = localizedText;
            };
            localizedString.RefreshString();
        }
    }
}
