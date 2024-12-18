using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ToggleMenuButton : MonoBehaviour
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
        var root = _buttonSelector.UI_Document.rootVisualElement;
        _button = root.Q<Button>(_buttonSelector.Name);
    }

    private void OnEnable()
    {
        _button.clicked += ToggleMenu;
    }

    private void OnDisable()
    {
        _button.clicked -= ToggleMenu;
    }

    private void ToggleMenu()
    {
        if (_menuVisibilityController.IsVisible) 
            _menuVisibilityController.Hide();
        else 
            _menuVisibilityController.Show();
    }

}
