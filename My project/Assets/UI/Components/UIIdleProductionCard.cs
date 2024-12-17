using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

public class UIIdleProductionCard : VisualElement
{
    private VisualElement _image;
    private Button _upgrade;
    private Label _type;
    private Label _spotsCount;
    private Label _productionPerSecond;
    private Label _productionIncrement;

    public Sprite Image
    {
        get => _image.style.backgroundImage.value.sprite;
        set => _image.style.backgroundImage = new StyleBackground(value);
    }

    public string Type
    {
        get => _type.text;
        set => _type.text = value;
    }

    public string SpotsCount
    {
        get => _spotsCount.text;
        set => _spotsCount.text = value;
    }

    public string ProductionPerSecond
    {
        get => _productionPerSecond.text;
        set => _productionPerSecond.text = value;
    }

    public string ProductionIncrement
    {
        get => _productionIncrement.text;
        set => _productionIncrement.text = value;
    }

    public string Price
    {
        get => _upgrade.text;
        set => _upgrade.text = value;
    }

    public Button UpgradeButton
    {
        get => _upgrade;
        set => _upgrade = value;
    }

    //--------------------------------------------------------------------------
    /* Resolve the link between the UXML template and the Custom Controller. */
    public UIIdleProductionCard()
    {
        var visualTree = Addressables.LoadAssetAsync<VisualTreeAsset>("Assets/UI/Components/UXMLIdleProductionCard.uxml").WaitForCompletion();
        visualTree.CloneTree(this);

        _image = this.Q<VisualElement>("factory-image");
        _upgrade = this.Q<Button>("upgrade-button");
        _type = this.Q<Label>("factory-type-label");
        _spotsCount = this.Q<Label>("spots-count-label");
        _productionPerSecond = this.Q<Label>("production-per-second-label");
        _productionIncrement = this.Q<Label>("production-increment-label");
    }

    public new class UxmlFactory : UxmlFactory<UIIdleProductionCard, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlAssetAttributeDescription<Sprite> _image = new() { name = "image" };
        UxmlStringAttributeDescription _type = new() { name = "type", defaultValue = "Tulip" };
        UxmlStringAttributeDescription _spotsCount = new() { name = "spots-count", defaultValue = "5" };
        UxmlStringAttributeDescription _productionPerSecond = new() { name = "production-per-second", defaultValue = "20/s" };
        UxmlStringAttributeDescription _productionIncrement = new() { name = "production-increment", defaultValue = "100" };
        UxmlStringAttributeDescription _price = new() { name = "price", defaultValue = "100$" };


        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            get { yield break; }
        }

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var card = ve as UIIdleProductionCard;

            card.Image = _image.GetValueFromBag(bag, cc);
            card.Type = _type.GetValueFromBag(bag, cc);
            card.SpotsCount = _spotsCount.GetValueFromBag(bag, cc);
            card.ProductionPerSecond = _productionPerSecond.GetValueFromBag(bag, cc);
            card.ProductionIncrement = _productionIncrement.GetValueFromBag(bag, cc);
            card.Price = _price.GetValueFromBag(bag, cc);
        }
    }
}
