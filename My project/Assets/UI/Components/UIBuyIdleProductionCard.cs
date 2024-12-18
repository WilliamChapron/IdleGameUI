using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

public class UIBuyIdleProductionCard : VisualElement
{
    private Label _type;
    private VisualElement _image;
    private Button _buyButton;

    public string Type
    {
        get => _type.text;
        set => _type.text = value;
    }

    public Sprite Image
    {
        get => _image.style.backgroundImage.value.sprite;
        set => _image.style.backgroundImage = new StyleBackground(value);
    }

    public string PlotPrice
    {
        get => _buyButton.text;
        set => _buyButton.text = value;
    }

    public Button BuyButton
    {
        get => _buyButton;
        set => _buyButton = value;
    }

    //--------------------------------------------------------------------------
    /* Resolve the link between the UXML template and the Custom Controller. */
    public UIBuyIdleProductionCard()
    {
        var visualTree = Addressables.LoadAssetAsync<VisualTreeAsset>("UXMLBuyIdleProductionCard").WaitForCompletion();
        visualTree.CloneTree(this);

        _type = this.Q<Label>("idle-production-type");
        _image = this.Q<VisualElement>("idle-production-image");
        _buyButton = this.Q<Button>("buy-button");
    }

    public new class UxmlFactory : UxmlFactory<UIBuyIdleProductionCard, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlStringAttributeDescription _type = new() { name = "type", defaultValue = "Type" };
        UxmlAssetAttributeDescription<Sprite> _image = new() { name = "image" };
        UxmlStringAttributeDescription _plotPrice = new() { name = "plotPrice", defaultValue = "Price: 500$" };

        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            get { yield break; }
        }

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var card = ve as UIBuyIdleProductionCard;

            card.Type = _type.GetValueFromBag(bag, cc);
            card.Image = _image.GetValueFromBag(bag, cc);
            card.PlotPrice = _plotPrice.GetValueFromBag(bag, cc);
        }
    }

    public void OnPlotPriceChange(int plotPrice)
    {
        PlotPrice = plotPrice.ToString();
    }
}
