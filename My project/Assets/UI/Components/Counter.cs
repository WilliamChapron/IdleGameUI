using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

public class Counter : VisualElement
{
    private VisualElement _icon;
    private Label _count;
    private Label _unit;

    public Sprite Icon
    {
        get => _icon.style.backgroundImage.value.sprite;
        set => _icon.style.backgroundImage = new StyleBackground(value);
    }

    public string Count
    {
        get => _count.text;
        set => _count.text = value;
    }

    public string Unit
    {
        get => _unit.text;
        set => _unit.text = value;
    }

    //--------------------------------------------------------------------------
    /* Resolve the link between the UXML template and the Custom Controller. */
    public Counter()
    {
        var visualTree = Addressables.LoadAssetAsync<VisualTreeAsset>("Assets/UI/Components/UXMLCounter.uxml").WaitForCompletion();
        visualTree.CloneTree(this);

        _icon = this.Q<VisualElement>("icon");
        _count = this.Q<Label>("count");
        _unit = this.Q<Label>("unit");
    }

    public new class UxmlFactory : UxmlFactory<Counter, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlAssetAttributeDescription<Sprite> _icon = new() { name = "icon-attr" };
        UxmlStringAttributeDescription _count = new() { name = "count-attr", defaultValue = "000" };
        UxmlStringAttributeDescription _unit = new() { name = "unit-attr", defaultValue = "" };

        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            get { yield break; }
        }

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var counter = ve as Counter;

            counter.Icon = _icon.GetValueFromBag(bag, cc);
            counter.Count = _count.GetValueFromBag(bag, cc);
            counter.Unit = _unit.GetValueFromBag(bag, cc);
        }
    }
}
