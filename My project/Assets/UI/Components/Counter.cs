using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class Counter : VisualElement
{
    public new class UxmlFactory : UxmlFactory<Counter, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlAssetAttributeDescription<Sprite> _icon = new UxmlAssetAttributeDescription<Sprite> { name = "icon-attr" };
        UxmlStringAttributeDescription _count = new UxmlStringAttributeDescription { name = "count-attr", defaultValue = "000" };
        UxmlStringAttributeDescription _unit = new UxmlStringAttributeDescription { name = "unit-attr", defaultValue = "" };

        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            get { yield break; }
        }

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var counter = ve as Counter;

            counter.Clear();

            counter._iconAttr = _icon.GetValueFromBag(bag, cc);
            counter._counterAttr = _count.GetValueFromBag(bag, cc);
            counter.Add(new Label("counter") { text = counter._counterAttr });
            counter._unitAttr = _unit.GetValueFromBag(bag, cc);
            counter.Add(new Label("unit") { text = counter._unitAttr });
        }
    }

    public Sprite _iconAttr { get; set; }
    public string _counterAttr { get; set; }
    public string _unitAttr { get; set; }
}
