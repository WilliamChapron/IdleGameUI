using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class MarketPlaceFilterUI : MonoBehaviour
{
    [SerializeField] private UIDocument m_uiDocument;

    public VisualTreeAsset bonusItemTemplate;  // box template
    public VisualElement container;  // scroll viw container where we add box

    public VisualElement m_root;
    void Start()
    {
        m_root = m_uiDocument.rootVisualElement;
        var bonusItems = new List<(string Name, int Price)>()
        {
            ("Production X2", 5000),
            ("Double Rewards", 1500),
            ("Speed Boost", 1200)
        };

        container = m_root.Q<VisualElement>("bonus-scrollview");


        foreach (var item in bonusItems)
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
