using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIDocument mainUIDocument; 
    [SerializeField] private GameObject marketplaceUI; 

    private void Start()
    {
        var root = mainUIDocument.rootVisualElement;

        Button button2 = root.Q<Button>("icon2");

        button2.clicked += OnIcon2Clicked;
    }

    private void OnIcon2Clicked()
    {
        marketplaceUI.GetComponent<MarketPlaceMenuUIController>().ToggleMarketplace();
    }
}
