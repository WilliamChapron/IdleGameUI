using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public enum BonusType
{
    FactoryWide,
    Global
}

public class MarketPlaceFilterUI : MonoBehaviour
{
    [SerializeField] private UIDocument m_uiDocument;

    public VisualTreeAsset bonusItemTemplate;  // box template
    public VisualElement container;  // scroll view container where we add box

    public VisualElement m_root;
    private List<(string Name, int Price, BonusType Type)> bonusItems;

    private Label currentFilterLabel;

    void Start()
    {
        m_root = m_uiDocument.rootVisualElement;

  
        currentFilterLabel = m_root.Q<Label>("current-filter-type");

        bonusItems = new List<(string Name, int Price, BonusType Type)>
        {

            ("Production X2", 5000, BonusType.FactoryWide),
            ("Double Rewards", 1500, BonusType.FactoryWide),
            ("Speed Boost", 1200, BonusType.FactoryWide),
            ("Energy Efficiency", 2500, BonusType.FactoryWide),


            ("Weather Boost", 2000, BonusType.Global),
            ("Global Speed Increase", 1800, BonusType.Global)
        };

        container = m_root.Q<VisualElement>("bonus-scrollview");

        var allButton = m_root.Q<Button>("all-type");
        var factoryWideButton = m_root.Q<Button>("factorywide-type");
        var globalButton = m_root.Q<Button>("global-type");

        allButton.clicked += () => FilterBonus(new BonusType[] { BonusType.FactoryWide, BonusType.Global }, "All");
        factoryWideButton.clicked += () => FilterBonus(new BonusType[] { BonusType.FactoryWide }, "FactoryWide");
        globalButton.clicked += () => FilterBonus(new BonusType[] { BonusType.Global }, "Global");


        FilterBonus(new BonusType[] { BonusType.FactoryWide, BonusType.Global }, "All");
    }


    void FilterBonus(BonusType[] types, string filterName)
    {

        currentFilterLabel.text = $"Current Type: {filterName}";

        var filteredItems = bonusItems.FindAll(item => System.Array.Exists(types, type => item.Type == type));
        DisplayBonuses(filteredItems);
    }

    void DisplayBonuses(List<(string Name, int Price, BonusType Type)> items)
    {
        container.Clear();

        foreach (var item in items)
        {
            var bonusItemElement = bonusItemTemplate.CloneTree();

            var nameLabel = bonusItemElement.Q<Label>("bonus-name");
            nameLabel.text = item.Name;

            var priceLabel = bonusItemElement.Q<Label>("bonus-price");
            priceLabel.text = item.Price.ToString();

            container.Add(bonusItemElement);
        }
    }

    void Update()
    {
    }
}
