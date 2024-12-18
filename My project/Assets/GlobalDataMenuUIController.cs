using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GlobalDataMenuUIController : MonoBehaviour
{
    [SerializeField] private UIDocument uIDocument; // R�f�rence au UIDocument

    private VisualElement _generalDataContainer;
    private Button _toggleButton;

    private VisualElement _contentContainer;

    private bool _isOpen = true;

    void OnEnable()
    {

        var root = uIDocument.rootVisualElement;

        _generalDataContainer = root.Q<VisualElement>("generalDataContainer");
        _toggleButton = root.Q<Button>("toggleButton");

        _toggleButton.clicked += ToggleMenu;
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
