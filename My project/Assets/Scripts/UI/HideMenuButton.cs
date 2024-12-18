using UnityEngine;
using UnityEngine.UIElements;

public class HideMenuButton : MonoBehaviour
{
    [SerializeField] private MenuVisibilityController _menuVisibilityController;
    [SerializeField] private ButtonSelector _buttonSelector;

    [System.Serializable]
    private class ButtonSelector
    {
        public UIDocument UI_Document;
        public string Name;
    }
    private Button _button;

    private void Awake()
    {
        /*var root = _buttonSelector.UI_Document.rootVisualElement;
        _button = root.Q<Button>(_buttonSelector.Name);*/
    }

    private void OnEnable()
    {
        var root = _buttonSelector.UI_Document.rootVisualElement;
        _button = root.Q<Button>(_buttonSelector.Name);
        _button.clicked += HideMenu;
    }

    private void OnDisable()
    {
        var root = _buttonSelector.UI_Document.rootVisualElement;
        _button = root.Q<Button>(_buttonSelector.Name);
        _button.clicked -= HideMenu;
    }

    private void HideMenu()
    {
        if (_menuVisibilityController.IsVisible) 
            _menuVisibilityController.Hide();
    }

}
