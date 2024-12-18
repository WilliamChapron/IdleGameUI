using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuVisibilityController : MonoBehaviour
{

    [SerializeField] private ElementSelector _selector;
    [SerializeField] private bool _isVisible = false;
    [SerializeField] private float _hideDurationInSeconds = 1;

    [System.Serializable]
    private class ElementSelector
    {
        public UIDocument UI_Document;
        public string ContainerName;
    }

    private VisualElement _element;
    private Coroutine _hideCoroutine;

    public bool IsVisible => _isVisible;

    private void Awake()
    {
        var root = _selector.UI_Document.rootVisualElement;
        _element = root.Q<VisualElement>(_selector.ContainerName);
        if (_isVisible)
        {
            _element.style.display = DisplayStyle.Flex;
            _element.RemoveFromClassList("closed");
        }
        else
        {
            _element.style.display = DisplayStyle.None;
            _element.AddToClassList("closed");
        }
    }

    public void Show()
    {
        if(_hideCoroutine != null) StopCoroutine(_hideCoroutine);
        _isVisible = true;
        _element.style.display = DisplayStyle.Flex;
        _element.RemoveFromClassList("closed");
    }

    public void Hide()
    {
        _isVisible = false;
        _element.AddToClassList("closed");

        _hideCoroutine = StartCoroutine(HideCoroutine());
    }

    private IEnumerator HideCoroutine()
    {
        yield return new WaitForSeconds(_hideDurationInSeconds);
        _element.style.display = DisplayStyle.None;
    }
}
